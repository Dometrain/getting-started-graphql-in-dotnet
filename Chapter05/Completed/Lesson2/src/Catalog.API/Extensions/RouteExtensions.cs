using Microsoft.AspNetCore.Http.HttpResults;
using Path = System.IO.Path;

namespace Microsoft.Extensions.DependencyInjection;

public static class RouteExtensions
{
    public static IEndpointRouteBuilder MapImageRoute(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(
            "/api/products/{id:int}/image",
            async Task<Results<NotFound, PhysicalFileHttpResult>>([AsParameters] ImageParameters parameters) =>
            {
                var imageInfo = await parameters.GetImageInfoAsync();

                if (imageInfo is null)
                {
                    return TypedResults.NotFound();
                }

                return TypedResults.PhysicalFile(
                    imageInfo.Path,
                    imageInfo.MimeType,
                    lastModified: imageInfo.LastModified);
            });
        return builder;
    }

    private record ImageParameters(
        int Id,
        ProductService ProductService,
        ImageStorage ImageStorage,
        IWebHostEnvironment Environment,
        CancellationToken CancellationToken)
    {
        public async Task<ImageInfo?> GetImageInfoAsync()
        {
            var product = await ProductService.GetProductByIdAsync(Id, CancellationToken);

            if (product?.ImageFileName is null)
            {
                return null;
            }

            var path = ImageStorage.GetFilePath(product.ImageFileName);

            if (path is null)
            {
                return null;
            }

            var extension = Path.GetExtension(path);
            var mimeType = GetImageMimeTypeFromImageFileExtension(extension);
            var lastModified = File.GetLastWriteTime(path);

            return new ImageInfo(path, mimeType, lastModified);
        } 
    }
    
    private record ImageInfo(string Path, string MimeType, DateTime LastModified);
    
    private static string GetImageMimeTypeFromImageFileExtension(string extension)
        => extension switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".bmp" => "image/bmp",
            ".tiff" => "image/tiff",
            ".wmf" => "image/wmf",
            ".jp2" => "image/jp2",
            ".svg" => "image/svg+xml",
            ".webp" => "image/webp",
            _ => "application/octet-stream",
        };

}