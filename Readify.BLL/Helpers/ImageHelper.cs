using Microsoft.AspNetCore.Http;
using Readify.BLL.FileManagement;

namespace Readify.BLL.Helpers
{
    public static class ImageHelper
    {
        private static readonly string _uploadFolder = Path.Combine("Uploads", "BookImages");

        public static async Task<string> UploadImageAsync(IFormFile image, string ISBN)
        {
            return await DocumentsSettings.UploadFileAsync(image, ISBN, _uploadFolder);
        }

        public static void DeleteImage(string imagePath)
        {
            DocumentsSettings.DeleteFile(imagePath, _uploadFolder);
        }

        public static async Task<string> ConvertImageToBase64Async(string imagePath)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "BookImages", imagePath);
            if (!File.Exists(filePath)) return null!;

            var imageBytes = await File.ReadAllBytesAsync(filePath);
            return $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}
