using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveImageAsync(
            IFormFile file, 
            string folderPath, 
            string prefix, 
            int width = 600, 
            int height = 600, 
            long maxSizeInBytes = 5_000_000)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            return await FileUploadHelper.SaveFileAsync(
                _env,
                file,
                folderPath,
                prefix,
                width,
                height,
                maxSizeInBytes
            );
        }
    }
}