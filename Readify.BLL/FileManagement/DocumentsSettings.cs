using Microsoft.AspNetCore.Http;

namespace Readify.BLL.FileManagement
{
    public static class DocumentsSettings
    {
        public static async Task<string> UploadFileAsync(IFormFile file, string uniquePath, string folderName)
        {
            // 1. Get the location of the directory (path)
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            // Ensure directory exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 2. Generate a unique file name
            var fileName = $"{uniquePath}-{Path.GetFileName(file.FileName)}";

            // 3. Get the full file path
            var filePath = Path.Combine(folderPath, fileName);

            // 4. Upload the file asynchronously
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return fileName; // to be saved in the database
        }

        public static void DeleteFile(string fileUrl, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileUrl);

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    throw ex; // consider logging or wrapping the exception
                }
            }
        }
    }
}