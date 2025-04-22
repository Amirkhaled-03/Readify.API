using Readify.BLL.Helpers;

namespace Readify.BLL.Features.BookCategories.DTOs
{
    public class ManageCategoriesPageDto
    {
        public List<CategoryDto> Categories { get; set; } = new();
        public Metadata Metadata { get; set; } = new();
    }

}
