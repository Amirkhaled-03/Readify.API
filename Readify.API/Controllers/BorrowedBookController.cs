using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.BLL.Features.BorrowedBooks.DTOs;
using Readify.BLL.Features.BorrowedBooks.Services;
using Readify.BLL.Specifications.BorrowedBookSpec;

namespace Readify.API.Controllers
{
    public class BorrowedBookController : BaseController
    {
        private readonly IBorrowedBookService _borrowedBookService;

        public BorrowedBookController(IBorrowedBookService borrowedBookService)
        {
            _borrowedBookService = borrowedBookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrowedBooks([FromQuery] BorrowedBookSpecification specs)
        {
            var result = await _borrowedBookService.GetAllBorrowedBooksAsync(specs);

            return Ok(new ApiResponse<ManageBorrowedBooksDto>(200, "success", data: result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowedBook(int id)
        {
            var result = await _borrowedBookService.GetBorrowedBookByIdAsync(id);

            return Ok(new ApiResponse<BorrowedBookDto>(200, "success", result));
        }

        [HttpGet("myBorrowedBooks")]
        public async Task<IActionResult> GetUserBorrowedBooks()
        {
            var result = await _borrowedBookService.GetUserBorrowBooksAsync();

            if (result == null)
                return NotFound(new ApiResponse<object>(404, "User does not have borrowed books!"));

            return Ok(new ApiResponse<List<BorrowedBookDto>>(200, "success", data: result));
        }

        [HttpPut("updateStatus")]
        public async Task<IActionResult> UpdateBorrowedBookStatus(UpdateBorrowedBookStatusDto borrowedBookDto)
        {
            var errors = await _borrowedBookService.UpdateBorrowedBookStatusAsync(borrowedBookDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Bad Request", errors: errors));

            return StatusCode(201, new ApiResponse<List<string>>(201, "Borrowed book status updated successfully", new List<string>()));

        }
    }
}
