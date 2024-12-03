namespace eShop.Catalog.Services.Errors;

public class FileExtensionNotAllowedException(string extension)
    : Exception($"The file extension {extension} is not allowed.")
{
    private static readonly string[] _safeFileExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp",];

    public string Extension { get; } = extension;

    public IReadOnlyList<string> AllowedExtensions => _safeFileExtensions;
}