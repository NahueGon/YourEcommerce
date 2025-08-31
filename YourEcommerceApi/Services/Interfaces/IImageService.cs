namespace YourEcommerceApi.Services.Interfaces;

public interface IImageService
{
    /// <summary>
    /// Guarda una imagen en la carpeta indicada y devuelve la ruta relativa.
    /// </summary>
    /// <param name="file">Archivo a guardar</param>
    /// <param name="folderPath">Carpeta relativa dentro de wwwroot</param>
    /// <param name="prefix">Prefijo para el nombre del archivo</param>
    /// <param name="width">Ancho de la imagen final (opcional)</param>
    /// <param name="height">Alto de la imagen final (opcional)</param>
    /// <param name="maxSizeInBytes">Tamaño máximo permitido (opcional)</param>
    /// <returns>Ruta relativa de la imagen</returns>
    Task<string> SaveImageAsync(
        IFormFile file, 
        string folderPath, 
        string prefix, 
        int width = 600, 
        int height = 600, 
        long maxSizeInBytes = 5_000_000);
}