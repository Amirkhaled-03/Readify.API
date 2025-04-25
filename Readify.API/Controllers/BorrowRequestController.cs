using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.Book;
using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Features.BorrowRequest.Services;
using Readify.BLL.Specifications.BookSpec;
using Readify.BLL.Specifications.BorrowRequestSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{
    public class BorrowRequestController : BaseController
    {
        private readonly IBorrowRequestService _borrowRequestService;
        public BorrowRequestController(IBorrowRequestService borrowRequestService)
        {
            _borrowRequestService = borrowRequestService;
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Get all requests",
        Description = "This endpoint allows Admins and librarians to retrieve a paginated list of borrow requests. "
        )]
        public async Task<IActionResult> GetAllRequests([FromQuery] BorrowRequestSpecification specification)
        {
            var result = await _borrowRequestService.GetAllBorrowRequestsAsync(specification);

            return Ok(new ApiResponse<List<BorrowRequestDto>> (200, "success", data: result));
        }
        
        [HttpGet("myBorrowRequests")]
        public async Task<IActionResult> GetUserBorrowRequests()
        {
            var requests = await _borrowRequestService.GetUserBorrowRequestsAsync();

            if(requests == null)
                return NotFound(new ApiResponse<object>(404, "User does not have requests!"));

            return Ok(new ApiResponse<List<BorrowRequestDto>> (200, "success", data: requests));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var br = await _borrowRequestService.GetBorrowRequestByIdAsync(id);

            if (br == null)
                return NotFound(new ApiResponse<object>(404, "Request does not exist!"));

            return Ok(new ApiResponse<BorrowRequestDto>(200, "success", br));

        }

        [HttpPost("Add")]
        [SwaggerOperation(
        Summary = "Add a new borrow request",
        Description = "Allows users to add a new borrow request with bookId"
        )]
        public async Task<IActionResult> AddBorrowRequest([FromBody] CreateBorrowRequestDto createBorrowRequestDto)
        {
            var errors = await _borrowRequestService.CreateBorrowRequestAsync(createBorrowRequestDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Bad Request", errors: errors));

            return StatusCode(201, new ApiResponse<List<string>>(201, "Borrow request created successfully", new List<string>()));
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateRequestStatus([FromBody] UpdateBorrowRequestStatusDto requestDto)
        {
            var errors = await _borrowRequestService.UpdateBorrowRequestStatusAsync(requestDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Bad Request", errors: errors));

            return StatusCode(201, new ApiResponse<List<string>>(201, "Borrow request status updated successfully", new List<string>()));
        }

        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(
            Summary = "Delete a borrow request",
            Description = "Allows user to delete borrow request with its ID"
            )]
        public async Task<IActionResult> DeleteBorrowRequest(int id)
        {
            var errors = await _borrowRequestService.DeleteRequestByIdAsync(id);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Failed to delete borrow request", errors: errors));

            return Ok(new ApiResponse<List<string>>(200, "Borrow Request deleted successfully", new List<string>()));

        }
    }
}