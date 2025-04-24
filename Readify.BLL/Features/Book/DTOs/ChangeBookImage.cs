using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.Book.DTOs
{
    public class ChangeBookImage
    {
        [Required(ErrorMessage = "Book ID is required.")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "New image is required.")]
        //    [AllowedExtensionsAndSize(new[] { ".jpg", ".jpeg" }, 1)]
        public required IFormFile NewImage { get; set; }
    }
}