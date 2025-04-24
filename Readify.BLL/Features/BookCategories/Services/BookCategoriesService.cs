using Readify.BLL.Constants;
using Readify.BLL.Features.BookCategories.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.CategoriesSpec;
using Readify.DAL.Entities;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.BookCategories.Services
{
    public class BookCategoriesService : IBookCategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookCategoriesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ManageCategoriesPageDto> GetAllCategoriesAsync(CategoriesSpecification spec)
        {
            var totalCountSpec = new CategoriesSpecificationImpl(spec);
            totalCountSpec.IgnorePagination();

            int matchedCount = await _unitOfWork.CategoriesRepository.CountWithSpecAsync(totalCountSpec);
            int totalCount = await _unitOfWork.CategoriesRepository.CountAsync();

            if (matchedCount <= AppConstants.DefaultPageSize)
            {
                spec.PageIndex = 1;
            }

            var categories = await _unitOfWork.CategoriesRepository.GetWithSpecificationsAsync(new CategoriesSpecificationImpl(spec))
                ?? Enumerable.Empty<Category>();

            var categoriesDto = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });

            var pagination = new Pagination
            {
                PageIndex = spec.PageIndex,
                PageSize = spec.PageSize,
                TotalRecords = matchedCount,
                TotalPages = (int)Math.Ceiling((double)matchedCount / spec.PageSize)
            };

            return new ManageCategoriesPageDto
            {
                Categories = categoriesDto.ToList(),
                Metadata = new Metadata { Pagination = pagination }
            };
        }

        public CategoryDto? GetCategoryByIdAsync(int id)
        {
            var category = _unitOfWork.CategoriesRepository.GetByID(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<IReadOnlyList<CategoryDto>> GetCategoiesByBookId(int bookId)
        {
            var bookCategories = await _unitOfWork.BookCategoriesRepository.GetBookCategoriesByBookId(bookId)
                                ?? Array.Empty<Category>();


            return bookCategories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList().AsReadOnly();
        }

        public async Task<List<string>> AddCategoryAsync(AddCategoryDto dto)
        {
            var errors = new List<string>();

            var exists = await _unitOfWork.CategoriesRepository.GetSpecificColumnFirstOrDefaultAsync(c => c.Name == dto.Name);
            if (exists)
            {
                errors.Add("Category with the same name already exists.");
                return errors;
            }

            var category = new Category { Name = dto.Name };
            _unitOfWork.CategoriesRepository.AddEntity(category);

            var result = await _unitOfWork.SaveAsync();
            if (result <= 0)
                errors.Add("An error occurred while adding the category.");

            return errors;
        }

        public async Task<List<string>> UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            var errors = new List<string>();
            var category = _unitOfWork.CategoriesRepository.GetByID(dto.Id);
            if (category == null)
            {
                errors.Add("Category not found.");
                return errors;
            }

            var exists = await _unitOfWork.CategoriesRepository.GetSpecificColumnFirstOrDefaultAsync(c => c.Id != dto.Id && c.Name == dto.Name);
            if (exists)
            {
                errors.Add("Category with the same name already exists.");
                return errors;
            }

            category.Name = dto.Name;
            _unitOfWork.CategoriesRepository.UpdateEntity(category);

            var result = await _unitOfWork.SaveAsync();
            if (result <= 0)
                errors.Add("An error occurred while updating the category.");

            return errors;
        }

        public async Task<List<string>> DeleteCategoryAsync(int id)
        {
            var errors = new List<string>();

            var category = _unitOfWork.CategoriesRepository.GetByID(id);
            if (category == null)
            {
                errors.Add("Category not found.");
                return errors;
            }

            if (_unitOfWork.CategoriesRepository.DeleteEntity(id))
            {
                await _unitOfWork.SaveAsync();
                return errors;
            }

            errors.Add("Failed to delete the category.");
            return errors;
        }
    }
}