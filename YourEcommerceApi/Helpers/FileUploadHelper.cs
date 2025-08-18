using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

public static class FileUploadHelper
{
    public static async Task<string> SaveFileAsync(
        IWebHostEnvironment env,
        IFormFile file,
        string folderPathRelative,
        string filePrefix,
        int width = 600,
        int height = 600,
        long maxSizeInBytes = 5_000_000)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Archivo inválido.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(extension))
            throw new InvalidOperationException("Formato de imagen no permitido.");

        if (file.Length > maxSizeInBytes)
            throw new InvalidOperationException("El archivo es demasiado grande.");

        if (string.IsNullOrWhiteSpace(env.WebRootPath))
            throw new InvalidOperationException("WebRootPath está nulo. ¿Existe la carpeta wwwroot?");

        var folderFullPath = Path.Combine(env.WebRootPath, folderPathRelative);
        Directory.CreateDirectory(folderFullPath);

        var fileName = $"{filePrefix}_{Guid.NewGuid()}.webp";
        var fullPath = Path.Combine(folderFullPath, fileName);

        // Procesar imagen
        using var image = await Image.LoadAsync(file.OpenReadStream());

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Crop,              // Recorta para que quede exactamente cuadrada
            Size = new Size(width, height),
            Position = AnchorPositionMode.Center // Centra el recorte
        }));

        var encoder = new WebpEncoder
        {
            Quality = 75 // Puedes ajustar esto
        };

        await image.SaveAsync(fullPath, encoder);

        return Path.Combine(folderPathRelative, fileName).Replace("\\", "/");
    }
}
