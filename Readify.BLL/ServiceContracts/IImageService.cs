using Microsoft.AspNetCore.Http;

namespace Readify.BLL.ServiceContracts
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile image, string nId);
        void DeleteImage(string imagePath);
        Task<string> ConvertImageToBase64Async(string imagePath);
    }
}
