using Microsoft.AspNetCore.Mvc;
using Readify.API.Filters;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.BorrowedBook;
using Readify.BLL.Features.BorrowedBooks.DTOs;
using Readify.BLL.Features.BorrowedBooks.Services;
using Readify.BLL.Specifications.BorrowedBookSpec;
using Readify.BLL.Specifications.UserBorrowedBooksSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{
    public class BorrowedBookController : BaseController
    {
        private readonly IBorrowedBookService _borrowedBookService;

        public BorrowedBookController(IBorrowedBookService borrowedBookService)
        {
            _borrowedBookService = borrowedBookService;
        }

        #region Get All Borrowed Books

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all borrowed books",
            Description = "Retrieves a paginated and filtered list of borrowed books along with metadata.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ManageBorrowedBooksDto>))]
        [SwaggerResponseExample(200, typeof(BorrowedBookResponseExample))]
        public async Task<IActionResult> GetAllBorrowedBooks([FromQuery] BorrowedBookSpecification specs)
        {
            var result = await _borrowedBookService.GetAllBorrowedBooksAsync(specs);
            return Ok(new ApiResponse<ManageBorrowedBooksDto>(200, "Successfully retrieved borrowed books", data: result));
        }

        #endregion

        #region Get Borrowed Book By ID

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a borrowed book by ID",
            Description = "Retrieves detailed information about a single borrowed book by its ID.")]
        [SwaggerResponse(200, "Successfully retrieved borrowed book", typeof(ApiResponse<BorrowedBookDto>))]
        [SwaggerResponse(404, "Borrowed book not found", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(200, typeof(GetBorrowedBookByIdSuccessExample))]
        [SwaggerResponseExample(404, typeof(GetBorrowedBookByIdNotFoundExample))]
        public async Task<IActionResult> GetBorrowedBookById(int id)
        {
            var book = await _borrowedBookService.GetBorrowedBookByIdAsync(id);

            if (book == null)
                return NotFound(new ApiResponse<string>(404, "Borrowed book not found"));

            return Ok(new ApiResponse<BorrowedBookDto>(200, "Success", data: book));
        }

        #endregion

        #region Get Current User's Borrowed Books

        [HttpGet("MyBorrowedBooks")]
        [SwaggerOperation(
            Summary = "Get current user's borrowed books",
            Description = "Retrieves all borrowed books for the currently logged-in user.")]
        [SwaggerResponse(200, "Successfully retrieved borrowed books", typeof(ApiResponse<List<BorrowedBookDto>>))]
        [SwaggerResponse(404, "No borrowed books found for user", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(200, typeof(GetUserBorrowedBooksSuccessExample))]
        [SwaggerResponseExample(404, typeof(GetUserBorrowedBooksNotFoundExample))]
        public async Task<IActionResult> GetMyBorrowedBooks([FromQuery] UserBorrowedBooksSpecification specs)
        {
            var books = await _borrowedBookService.GetUserBorrowBooksAsync(specs);

            if (books == null || !books.BorrowedBooks.Any())
                return NotFound(new ApiResponse<string>(404, "No borrowed books found for the user"));

            return Ok(new ApiResponse<ManageBorrowedBooksDto>(200, "Successfully retrieved borrowed books", books));
        }

        #endregion

        #region Update Borrowed Book Status

        [RoleBasedAuthorization(UserType.Librarian)]
        [HttpPut("UpdateBorrowedBookStatus")]
        [SwaggerOperation(
            Summary = "Update the status of a borrowed book",
            Description = "Updates the status (e.g., Returned) of a borrowed book. If marked as Returned, it records the confirmation and return time.")]
        [SwaggerResponse(200, "Status updated successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Failed to update borrowed book status", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(UpdateBorrowedBookStatusSuccessExample))]
        [SwaggerResponseExample(400, typeof(UpdateBorrowedBookStatusErrorExample))]
        public async Task<IActionResult> UpdateBorrowedBookStatus([FromBody] UpdateBorrowedBookStatusDto bookDto)
        {
            var errors = await _borrowedBookService.UpdateBorrowedBookStatusAsync(bookDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Failed to update borrowed book status", errors: errors));

            return Ok(new ApiResponse<string>(200, "Status updated successfully"));
        }

        #endregion
    }
}
