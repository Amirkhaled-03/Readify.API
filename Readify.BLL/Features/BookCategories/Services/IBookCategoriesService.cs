using Readify.BLL.Features.BookCategories.DTOs;
using Readify.BLL.Specifications.CategoriesSpec;

namespace Readify.BLL.Features.BookCategories.Services
{
    public interface IBookCategoriesService
    {
        Task<ManageCategoriesPageDto> GetAllCategoriesAsync(CategoriesSpecification spec);
        CategoryDto? GetCategoryByIdAsync(int id);
        Task<List<string>> AddCategoryAsync(AddCategoryDto dto);
        Task<List<string>> UpdateCategoryAsync(UpdateCategoryDto dto);
        Task<List<string>> DeleteCategoryAsync(int id);
    }
}