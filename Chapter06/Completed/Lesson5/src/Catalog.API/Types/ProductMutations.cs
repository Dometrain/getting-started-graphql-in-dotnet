using eShop.Catalog.Services.Errors;
using eShop.Catalog.Types.Errors;
using eShop.Catalog.Types.Inputs;
using HotChocolate.Subscriptions;
using Path = System.IO.Path;

namespace eShop.Catalog.Types;

[MutationType]
public static class ProductMutations
{
    [Error<InvalidBrandIdError>]
    [Error<InvalidProductTypeIdError>]
    [Error<ArgumentException>]
    [Error<MaxStockThresholdToSmallException>]
    public static async Task<Product> CreateProductAsync(
        CreateProductInput input,
        ProductService productService,
        ITopicEventSender topicEventSender,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            TypeId = input.TypeId,
            BrandId = input.BrandId,
            RestockThreshold = input.RestockThreshold,
            MaxStockThreshold = input.MaxStockThreshold
        };

        await productService.CreateProductAsync(product, cancellationToken);
        
        await topicEventSender.SendAsync(
            TopicNames.NewProductArrival,
            new NewProductArrivalEventMessage(product.Id, product.BrandId),
            cancellationToken);

        return product;
    }

    [Error<InvalidBrandIdError>]
    [Error<InvalidProductTypeIdError>]
    [Error<ArgumentException>]
    [Error<MaxStockThresholdToSmallException>]
    public static async Task<FieldResult<Product, InvalidProductIdError>> UpdateProductAsync(
        UpdateProductInput input,
        ProductService productService,
        CancellationToken cancellationToken)
    {
        var product = await productService.GetProductByIdAsync(input.Id, cancellationToken);

        if (product is null)
        {
            return new InvalidProductIdError(input.Id);
        }

        if (input.Name.HasValue)
        {
            product.Name = input.Name.Value;
        }

        if (input.Description.HasValue)
        {
            product.Description = input.Description.Value;
        }

        if (input.Price.HasValue)
        {
            product.Price = input.Price.Value;
        }

        if (input.TypeId.HasValue)
        {
            product.TypeId = input.TypeId.Value;
        }

        if (input.BrandId.HasValue)
        {
            product.BrandId = input.BrandId.Value;
        }

        if (input.RestockThreshold.HasValue)
        {
            product.RestockThreshold = input.RestockThreshold.Value;
        }

        if (input.MaxStockThreshold.HasValue)
        {
            product.MaxStockThreshold = input.MaxStockThreshold.Value;
        }

        await productService.UpdateProductAsync(product, cancellationToken);

        return product;
    }

    [Error<FileExtensionNotAllowedException>]
    public static async Task<FieldResult<Product, InvalidProductIdError>> UploadProductImageAsync(
        [ID<Product>] int id,
        IFile file,
        ProductService productService,
        ImageStorage imageStorage,
        CancellationToken cancellationToken)
    {
        var product = await productService.GetProductByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return new InvalidProductIdError(id);
        }

        await using var stream = file.OpenReadStream();
        product.ImageFileName = await imageStorage.SaveImageAsync(file.Name, stream, cancellationToken);
        await productService.UpdateProductAsync(product, cancellationToken);
        
        return product;
    }

    [UseMutationConvention(PayloadFieldName = "transactionId")]
    [Error<FileExtensionNotAllowedException>]
    public static async Task<FieldResult<string, InvalidProductIdError>> BeginUploadProductImageAsync(
        [ID<Product>] int id,
        IFile file,
        ProductService productService,
        ITopicEventSender topicEventSender,
        CancellationToken cancellationToken)
    {
        var product = await productService.GetProductByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return new InvalidProductIdError(id);
        }

        var transactionId = Guid.NewGuid().ToString("N");
        var fileName =  Path.GetTempFileName();
        
        await using var sourceStream = file.OpenReadStream();
        await using var tempFileStream = File.Open(fileName, FileMode.OpenOrCreate);
        await sourceStream.CopyToAsync(tempFileStream, cancellationToken);

        await topicEventSender.SendAsync(
            TopicNames.ImageProcessing, 
            new ProductImageEventMessage(transactionId, product.Id, fileName, file.Name), 
            cancellationToken);

        return transactionId;
    }
}

public record ProductImageEventMessage(string TransactionId, int ProductId, string FileName, string OriginalFileName);