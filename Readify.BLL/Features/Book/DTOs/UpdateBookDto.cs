using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.Book.DTOs
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public required string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN must be exactly 13 characters long.")]
        public required string ISBN { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Count must be at least 1.")]
        public required int Count { get; set; }

        //[MinLength(1, ErrorMessage = "At least one category must be selected.")]
        //public List<int> CategoriesIds { get; set; } = new List<int>();

        //public IFormFile? Image { get; set; }
    }
}
