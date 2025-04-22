using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.BookCategories.DTOs
{
    public class AddCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be at least 1 character long.")]
        public required string Name { get; set; }
    }
}
