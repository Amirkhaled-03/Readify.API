using Readify.API.HandleResponses;
using Readify.BLL.Features.BookCategories.DTOs;
using Readify.BLL.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.Category
{
    public class ManageCategoriesPageExample : IExamplesProvider<ApiResponse<ManageCategoriesPageDto>>
    {
        public ApiResponse<ManageCategoriesPageDto> GetExamples()
        {
            return new ApiResponse<ManageCategoriesPageDto>(200, "success", data: new ManageCategoriesPageDto
            {
                Categories = new List<CategoryDto>
                {
                    new CategoryDto { Id = 1, Name = "Software Engineering" },
                    new CategoryDto { Id = 2, Name = "Self Development" },
                    new CategoryDto { Id = 3, Name = "Artificial Intelligence" },
                    new CategoryDto { Id = 4, Name = "Comedy" }
                },
                Metadata = new Metadata
                {
                    Pagination = new Pagination
                    {
                        PageIndex = 1,
                        PageSize = 10,
                        TotalRecords = 4,
                        TotalPages = 1
                    }
                }
            });
        }

        public class GetCategoryByIdExample : IExamplesProvider<ApiResponse<CategoryDto>>
        {
            public ApiResponse<CategoryDto> GetExamples()
            {
                return new ApiResponse<CategoryDto>(200, "success", new CategoryDto
                {
                    Id = 1,
                    Name = "Software Engineering"
                });
            }
        }

        public class GetCategoryByIdNotFoundExample : IExamplesProvider<ApiResponse<string>>
        {
            public ApiResponse<string> GetExamples()
            {
                return new ApiResponse<string>(404, "Category not found.");
            }
        }

        public class AddCategorySuccessExample : IExamplesProvider<ApiResponse<string>>
        {
            public ApiResponse<string> GetExamples()
            {
                return new ApiResponse<string>(201, "Category added successfully.");
            }
        }

        public class AddCategoryErrorExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(400, "Bad request", new List<string>
            {
                "Category with the same name already exists."
            });
            }
        }

        public class UpdateCategorySuccessExample : IExamplesProvider<ApiResponse<string>>
        {
            public ApiResponse<string> GetExamples()
            {
                return new ApiResponse<string>(200, "Category updated successfully.");
            }
        }

        public class UpdateCategoryErrorExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(400, "Bad request", new List<string>
            {
                "Category with the same name already exists."
            });
            }
        }

        public class DeleteCategorySuccessExample : IExamplesProvider<ApiResponse<string>>
        {
            public ApiResponse<string> GetExamples()
            {
                return new ApiResponse<string>(200, "Category deleted successfully.");
            }
        }

        public class DeleteCategoryFailureExample : IExamplesProvider<ApiResponse<List<string>>>
        {
            public ApiResponse<List<string>> GetExamples()
            {
                return new ApiResponse<List<string>>(400, "Bad request", new List<string>
            {
                "Failed to delete the category."
            });
            }
        }

        public class GetCategoriesByBookIdSuccessExample : IExamplesProvider<ApiResponse<IReadOnlyList<CategoryDto>>>
        {
            public ApiResponse<IReadOnlyList<CategoryDto>> GetExamples()
            {
                return new ApiResponse<IReadOnlyList<CategoryDto>>(200, "Success", new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Software Engineering" },
                new CategoryDto { Id = 2, Name = "Programming" }
            });
            }
        }

        public class GetCategoriesByBookIdEmptyExample : IExamplesProvider<ApiResponse<IReadOnlyList<CategoryDto>>>
        {
            public ApiResponse<IReadOnlyList<CategoryDto>> GetExamples()
            {
                return new ApiResponse<IReadOnlyList<CategoryDto>>(200, "No categories assigned to this book", new List<CategoryDto>());
            }
        }

    }
}
