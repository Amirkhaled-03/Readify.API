using Microsoft.AspNetCore.Http;

namespace Readify.BLL.FileManagement
{
    public class ImageService : IImageService
    {
        private static readonly string _uploadFolder = Path.Combine("Uploads", "EmployeeImages");

        public async Task<string> UploadImageAsync(IFormFile image, string nId)
        {
            return await DocumentsSettings.UploadFileAsync(image, nId, _uploadFolder);
        }

        public void DeleteImage(string imagePath)
        {
            DocumentsSettings.DeleteFile(imagePath, _uploadFolder);
        }

        public async Task<string> ConvertImageToBase64Async(string imagePath)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "EmployeeImages", imagePath);
            if (!File.Exists(filePath)) return null!;

            var imageBytes = await File.ReadAllBytesAsync(filePath);
            return $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}
