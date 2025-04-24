using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.Book.DTOs
{
    public class AddCategoriesToBookDto
    {
        [Required(ErrorMessage = "At least one category must be selected.")]
        [MinLength(1, ErrorMessage = "At least one category must be selected.")]
        public required List<int> CategoriesIds { get; set; }


        [Required(ErrorMessage = "Book ID is required.")]
        public required int BookId { get; set; }
    }
}