using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.Book.DTOs
{
    public class AddBookDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public required string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN must be exactly 13 characters long.")]
        public required string ISBN { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Count must be at least 1.")]
        public required int Count { get; set; }

        [Required(ErrorMessage = "Book image is required.")]
        public required IFormFile Image { get; set; }

        [MinLength(1, ErrorMessage = "At least one category must be selected.")]
        public List<int> CategoriesIds { get; set; } = new List<int>();
    }
}
