using Microsoft.AspNetCore.Mvc;
using Readify.API.Filters;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.BorrowRequest;
using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Features.BorrowRequest.Services;
using Readify.BLL.Specifications.BorrowRequestSpec;
using Readify.BLL.Specifications.UserBorrowRequestSpec;
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

        #region Get All Borrow Requests

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieve all borrow requests",
            Description = "This endpoint returns all borrow requests with optional filtering and pagination.")]
        [SwaggerResponse(200, "Borrow requests retrieved successfully", typeof(ApiResponse<ListAllRequestsDto>))]
        [SwaggerResponse(400, "Bad request due to invalid parameters", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(GetAllBorrowRequestsSuccessExample))]
        [SwaggerResponseExample(400, typeof(GetAllBorrowRequestsErrorExample))]
        public async Task<IActionResult> GetAllBorrowRequests([FromQuery] BorrowRequestSpecification specs)
        {
            var response = await _borrowRequestService.GetAllBorrowRequestsAsync(specs);

            //if (response == null || !response.BorrowRequests.Any())
            //    return NotFound(new ApiResponse<string>(404, "No borrow requests found"));

            return Ok(new ApiResponse<ListAllRequestsDto>(200, "Borrow requests retrieved successfully", response));
        }

        #endregion

        #region Get User Borrow Requests

        [RoleBasedAuthorization(UserType.User)]
        [HttpGet("myBorrowRequests")]
        [SwaggerOperation(
            Summary = "Retrieve all borrow requests for the currently authenticated user",
            Description = "This endpoint allows users to retrieve all of their borrow requests.")]
        [SwaggerResponse(200, "success", typeof(ApiResponse<List<BorrowRequestDto>>))]
        [SwaggerResponse(404, "No borrow requests found for user", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(200, typeof(GetUserBorrowRequestsSuccessExample))]
        [SwaggerResponseExample(404, typeof(GetUserBorrowRequestsErrorExample))]
        public async Task<IActionResult> GetUserBorrowRequests([FromQuery] UserBorrowRequestSpecification specs)
        {
            var response = await _borrowRequestService.GetUserBorrowRequestsAsync(specs);

            return Ok(new ApiResponse<ListAllRequestsDto>(200, "success", response));
        }

        #endregion

        #region Get Borrow Request By ID

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Retrieve a borrow request by its ID",
            Description = "This endpoint allows you to retrieve a specific borrow request using its ID.")]
        [SwaggerResponse(200, "success", typeof(ApiResponse<BorrowRequestDto>))]
        [SwaggerResponse(404, "Borrow request not found", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(200, typeof(GetBorrowRequestByIdSuccessExample))]
        [SwaggerResponseExample(404, typeof(GetBorrowRequestByIdErrorExample))]
        public async Task<IActionResult> GetBorrowRequestById(int id)
        {
            var response = await _borrowRequestService.GetBorrowRequestByIdAsync(id);

            if (response == null)
                return NotFound(new ApiResponse<string>(404, "Borrow request not found"));

            return Ok(new ApiResponse<BorrowRequestDto>(200, "success", response));
        }

        #endregion

        #region Create Borrow Request

        [RoleBasedAuthorization(UserType.User)]
        [HttpPost("Add")]
        [SwaggerOperation(
            Summary = "Create a new borrow request for a user",
            Description = "This endpoint allows authenticated users to create a borrow request for a book.")]
        [SwaggerResponse(200, "Borrow request created successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Invalid input or failed validation", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(CreateBorrowRequestSuccessExample))]
        [SwaggerResponseExample(400, typeof(CreateBorrowRequestErrorExample))]
        public async Task<IActionResult> CreateBorrowRequest([FromBody] CreateBorrowRequestDto createBorrowRequestDto)
        {
            var errors = await _borrowRequestService.CreateBorrowRequestAsync(createBorrowRequestDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Invalid input or failed validation", errors));

            return Ok(new ApiResponse<string>(200, "Borrow request created successfully"));
        }

        #endregion

        #region Update Borrow Request Status

        [RoleBasedAuthorization(UserType.Librarian)]
        [HttpPut("UpdateStatus")]
        [SwaggerOperation(
            Summary = "Update the status of a borrow request",
            Description = "This endpoint allows updating the status of a borrow request, and it can trigger additional actions, like adding a borrowed book and decrementing available copies.")]
        [SwaggerResponse(200, "Borrow request status updated successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Failed validation or request not found", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(UpdateBorrowRequestStatusSuccessExample))]
        [SwaggerResponseExample(400, typeof(UpdateBorrowRequestStatusErrorExample))]
        public async Task<IActionResult> UpdateBorrowRequestStatus([FromBody] UpdateBorrowRequestStatusDto requestDto)
        {
            var errors = await _borrowRequestService.UpdateBorrowRequestStatusAsync(requestDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Failed validation or request not found", errors));

            return Ok(new ApiResponse<string>(200, "Borrow request status updated successfully"));
        }

        #endregion

        #region Delete Borrow Request By ID

        [RoleBasedAuthorization(UserType.User)]
        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(
            Summary = "Delete a borrow request by ID",
            Description = "This endpoint allows the deletion of a borrow request by its ID.")]
        [SwaggerResponse(200, "Borrow request deleted successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Failed validation or request not found", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(DeleteBorrowRequestSuccessExample))]
        [SwaggerResponseExample(400, typeof(DeleteBorrowRequestErrorExample))]
        public async Task<IActionResult> DeleteBorrowRequestById([FromRoute] int id)
        {
            var errors = await _borrowRequestService.DeleteRequestByIdAsync(id);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Failed validation or request not found", errors));

            return Ok(new ApiResponse<string>(200, "Borrow request deleted successfully"));
        }

        #endregion
    }
}