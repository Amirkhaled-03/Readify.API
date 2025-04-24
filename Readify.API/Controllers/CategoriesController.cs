using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.Category;
using Readify.BLL.Features.BookCategories.DTOs;
using Readify.BLL.Features.BookCategories.Services;
using Readify.BLL.Specifications.CategoriesSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using static Readify.API.ResponseExample.Category.ManageCategoriesPageExample;

namespace Readify.API.Controllers
{

    public class CategoriesController : BaseController
    {
        private readonly IBookCategoriesService _categoryService;
        public CategoriesController(IBookCategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        #region Get All categories 

        [HttpGet]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ManageCategoriesPageDto>))]
        [SwaggerResponseExample(200, typeof(ManageCategoriesPageExample))]
        [SwaggerOperation(
        Summary = "Get all categories",
        Description = "Retrieves a paginated list of categories with filtering options."
        )]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoriesSpecification spec)
        {
            var result = await _categoryService.GetAllCategoriesAsync(spec);
            return Ok(new ApiResponse<ManageCategoriesPageDto>(200, "success", data: result));
        }


        #endregion

        #region Get by Id 

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<CategoryDto>))]
        [SwaggerResponse(404, "Not Found", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(200, typeof(GetCategoryByIdExample))]
        [SwaggerResponseExample(404, typeof(GetCategoryByIdNotFoundExample))]
        [SwaggerOperation(
        Summary = "Get category by ID",
        Description = "Fetch a specific category using its unique identifier."
        )]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound(new ApiResponse<string>(404, "Category not found."));

            return Ok(new ApiResponse<CategoryDto>(200, "success", category));
        }

        #endregion

        #region Add 

        [HttpPost("AddCategory")]
        [SwaggerResponse(201, "Success", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Validation errors", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(201, typeof(AddCategorySuccessExample))]
        [SwaggerResponseExample(400, typeof(AddCategoryErrorExample))]
        [SwaggerOperation(
        Summary = "Add new category",
        Description = "This endpoint allows adding a new book category. The name must be unique."
        )]

        public async Task<IActionResult> AddCategory(AddCategoryDto dto)
        {
            var errors = await _categoryService.AddCategoryAsync(dto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Bad request", errors));

            return CreatedAtAction(nameof(AddCategory), new ApiResponse<string>(201, "Category added successfully."));
        }

        #endregion

        #region Update

        [HttpPut("UpdateCategory")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Validation errors", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(UpdateCategorySuccessExample))]
        [SwaggerResponseExample(400, typeof(UpdateCategoryErrorExample))]
        [SwaggerOperation(
        Summary = "Update a category",
        Description = "Updates a category's name. Returns errors if not found, name is taken, or save fails."
        )]

        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            var errors = await _categoryService.UpdateCategoryAsync(dto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Bad request", errors));

            return Ok(new ApiResponse<string>(200, "Category updated successfully."));
        }

        #endregion

        #region delete 

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Validation errors", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(DeleteCategorySuccessExample))]
        [SwaggerResponseExample(400, typeof(DeleteCategoryFailureExample))]
        [SwaggerOperation(
        Summary = "Delete a category",
        Description = "Deletes a category by ID. Returns errors if not found or if deletion fails."
        )]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var errors = await _categoryService.DeleteCategoryAsync(id);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Bad request", errors));

            return Ok(new ApiResponse<string>(200, "Category deleted successfully."));
        }

        #endregion

        #region Get book Categories 

        [HttpGet("{bookId}/categories")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<IReadOnlyList<CategoryDto>>))]
        [SwaggerResponseExample(200, typeof(GetCategoriesByBookIdSuccessExample))]
        [SwaggerOperation(
        Summary = "Get categories assigned to a book",
        Description = "Returns all categories that are assigned to the specified book."
        )]
        public async Task<IActionResult> GetCategoriesByBookId(int bookId)
        {
            var categories = await _categoryService.GetCategoiesByBookId(bookId);
            var message = categories.Any() ? "Success" : "No categories assigned to this book";
            return Ok(new ApiResponse<IReadOnlyList<CategoryDto>>(200, message, categories));
        }


        #endregion

    }
}
