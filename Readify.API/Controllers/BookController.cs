using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.Book;
using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Features.Book.Services;
using Readify.BLL.Specifications.BookSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        #region Get All Books

        [HttpGet]
        [SwaggerResponse(200, "success", typeof(ApiResponse<ManageBooksPageDto>))]
        [SwaggerResponseExample(200, typeof(ManageBooksPageExample))]
        [SwaggerOperation(
        Summary = "Get all Books",
        Description = "This endpoint allows Admins to retrieve a paginated list of books. " +
                   "You can filter results using query parameters such as Book Title, Author, ISBN, Category, or ID. " +
                   "Search fields are defined in the BookSpecifications class and utilize constants from Readify.BLL.Constants for default values."
        )]
        public async Task<IActionResult> GetAll([FromQuery] BookSpecifications specification)
        {
            var result = await _bookService.GetAllBooksAsync(specification);

            return Ok(new ApiResponse<ManageBooksPageDto>(200, "success", data: result));
        }

        #endregion

        #region Get Book by id

        [HttpGet("{bookId}")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<BookDetailsDto>))]
        [SwaggerResponse(404, "Not Found", typeof(ApiResponse<object>))]
        [SwaggerResponseExample(200, typeof(GetBookDetailsSuccessExample))]
        [SwaggerResponseExample(404, typeof(GetBookDetailsNotFoundExample))]
        [SwaggerOperation(
            Summary = "Get book details by ID",
            Description = "Returns detailed information about a specific book, including categories and metadata."
        )]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var result = await _bookService.GetBookDetails(bookId);

            if (result == null)
                return NotFound(new ApiResponse<object>(404, "Book not found"));

            return Ok(new ApiResponse<BookDetailsDto>(200, "success", result));
        }

        #endregion

        #region Add Book 

        [HttpPost("Add")]
        [SwaggerResponse(201, "Book Created", typeof(ApiResponse<List<string>>))]
        [SwaggerResponse(400, "Validation Failed", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(201, typeof(AddBookSuccessExample))]
        [SwaggerResponseExample(400, typeof(AddBookFailedExample))]
        [SwaggerOperation(
        Summary = "Add a new book",
        Description = "Allows admins to add a new book with image, categories, and availability count."
        )]

        public async Task<IActionResult> AddBook([FromQuery] AddBookDto bookDto)
        {
            var errors = await _bookService.AddBook(bookDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "bad request", errors: errors));

            return StatusCode(201, new ApiResponse<List<string>>(201, "Book created successfully", new List<string>()));
        }

        #endregion

        #region Update Book 

        [HttpPut("Update")]
        [SwaggerResponse(200, "Book Updated", typeof(ApiResponse<List<string>>))]
        [SwaggerResponse(400, "Failed to Update Book", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(UpdateBookSuccessExample))]
        [SwaggerResponseExample(400, typeof(UpdateBookFailedExample))]
        [SwaggerOperation(
        Summary = "Update existing book",
        Description = "Allows admins to update book info including title, author, ISBN, and count."
        )]
        public async Task<IActionResult> UpdateBook(UpdateBookDto bookDto)
        {
            var errors = await _bookService.UpdateBook(bookDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "bad request", errors: errors));

            return Ok(new ApiResponse<List<string>>(200, "Book updated successfully", new List<string>()));
        }

        #endregion

        #region Delete 

        [HttpDelete("Delete/{id}")]
        [SwaggerResponse(200, "Book Deleted", typeof(ApiResponse<List<string>>))]
        [SwaggerResponse(400, "Failed to Delete Book", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(DeleteBookSuccessExample))]
        [SwaggerResponseExample(400, typeof(DeleteBookFailedExample))]
        [SwaggerOperation(
        Summary = "Delete a book by its ID",
        Description = "This endpoint allows admins to delete a book, with checks to prevent deletion of borrowed books or books with active requests."
        )]

        public async Task<IActionResult> DeleteById(int id)
        {
            var errors = await _bookService.DeleteById(id);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Failed to delete book", errors: errors));

            return Ok(new ApiResponse<List<string>>(200, "Book deleted successfully", new List<string>()));
        }


        #endregion
    }

}