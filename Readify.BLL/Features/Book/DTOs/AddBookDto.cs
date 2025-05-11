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
        [Range(1000, 2030, ErrorMessage = "Publish year must be between 1000 and 2030.")]
        public int PublishYear { get; set; }

        [Required(ErrorMessage = "Language is required.")]
        [StringLength(50, ErrorMessage = "Language cannot exceed 50 characters.")]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Page Count is required.")]
        [Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10,000.")]
        public int PageCount { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.0, 10000.0, ErrorMessage = "Price must be between 0 and 10,000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        public double Rating { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Count must be at least 1.")]
        public required int Count { get; set; }

        [Required(ErrorMessage = "Book image is required.")]
        public required IFormFile Image { get; set; }

        [MinLength(1, ErrorMessage = "At least one category must be selected.")]
        public List<int> CategoriesIds { get; set; } = new List<int>();
    }
}
