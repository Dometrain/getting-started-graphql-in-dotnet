using System.Diagnostics;
using eShop.Catalog.Services.Errors;
using ISession = eShop.Catalog.Sessions.ISession;

namespace eShop.Catalog.Services;

public sealed class BrandService(
    CatalogContext context,
    IBrandByIdDataLoader brandById,
    IBrandByNameDataLoader brandByName,
    ISession session)
{
    public async Task<Brand?> GetBrandByIdAsync(
        int id,
        CancellationToken ct = default)
        => await brandById.LoadAsync(id, ct);

    public async Task<Brand?> GetBrandByNameAsync(
        string name,
        CancellationToken ct = default)
        => await brandByName.LoadAsync(name, ct);

    public async Task<Page<Brand>> GetBrandsAsync(
        PagingArguments args,
        CancellationToken ct = default)
    {
        using var source = ActivitySources.Source.StartActivity();
        source?.SetTag("first", args.First);
        
        var page = await context.Brands
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .ToPageAsync(args, ct);

        foreach (var brand in page.Items)
        {
            brandById.Set(brand.Id, brand);
        }

        return page;
    }

    public async Task CreateBrandAsync(Brand brand, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(brand.Name))
        {
            ArgumentException.ThrowIfNullOrEmpty(brand.Name);
        }
        
        context.Brands.Add(brand);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateBrandAsync(Brand brand, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(brand.Name))
        {
            ArgumentException.ThrowIfNullOrEmpty(brand.Name);
        }

        context.Brands.Entry(brand).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> DeleteBrandAsync(int id, CancellationToken cancellationToken)
    {
        var affectedRows = await context.Brands.Where(t => t.Id == id).ExecuteDeleteAsync(cancellationToken);
        return affectedRows > 0;
    }

    public async Task SubscribeToBrandAsync(int brandId, CancellationToken cancellationToken)
    {
        if (session.CurrentUser is null)
        {
            throw new UserSessionRequiredException();
        }
        
        if (!await context.Brands.AnyAsync(t => t.Id == brandId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Brand), brandId);
        }

        if (await context.BrandSubscriptions.AnyAsync(
                t => t.UserId == session.CurrentUser.Id && 
                     t.BrandId == brandId, cancellationToken))
        {
            throw new AlreadySubscribedException(session.CurrentUser.Id, brandId);
        }

        try
        {
            await context.BrandSubscriptions.AddAsync(
                new BrandSubscription { UserId = session.CurrentUser.Id, BrandId = brandId },
                cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new AlreadySubscribedException(session.CurrentUser.Id, brandId, ex);
        }
    }
    
    public async Task UnsubscribeToBrandAsync(int brandId, CancellationToken cancellationToken)
    {
        if (session.CurrentUser is null)
        {
            throw new UserSessionRequiredException();
        }
        
        if (!await context.Brands.AnyAsync(t => t.Id == brandId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Brand), brandId);
        }

        if (!await context.BrandSubscriptions.AnyAsync(
                t => t.UserId == session.CurrentUser.Id && 
                     t.BrandId == brandId, cancellationToken))
        {
            return;
        }

        await context.BrandSubscriptions
            .Where(t => t.UserId == session.CurrentUser.Id && t.BrandId == brandId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}


public static class ActivitySources
{
    public static ActivitySource Source { get; } = new("eShop.Catalog");
}