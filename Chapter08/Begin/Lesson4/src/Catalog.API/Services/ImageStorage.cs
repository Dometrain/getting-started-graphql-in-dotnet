using eShop.Catalog.Services.Errors;
using Path = System.IO.Path;

namespace eShop.Catalog.Services;

public sealed class ImageStorage
{
    private readonly string _imageDirectory =
        Path.Combine(Directory.GetCurrentDirectory(), "Images");
    private readonly HashSet<string> _safeExtensions =
        [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp",];

    public async Task<string> SaveImageAsync(
        string name,
        Stream stream,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(stream);

        var extension = Path.GetExtension(name);
        if (!_safeExtensions.Contains(extension))
        {
            throw new FileExtensionNotAllowedException(extension);
        }

        if (!Directory.Exists(_imageDirectory))
        {
            Directory.CreateDirectory(_imageDirectory);
        }

        var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(name)}";
        var filePath = Path.Combine(_imageDirectory, fileName);

        await using var fileStream = File.Create(filePath);
        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(fileStream, cancellationToken);

        return fileName;
    }

    public string? GetFilePath(string name)
    {
        var filePath = Path.Combine(_imageDirectory, name);
        if (File.Exists(filePath))
        {
            return filePath;
        }

        return null;
    }
}