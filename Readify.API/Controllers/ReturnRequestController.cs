using Microsoft.AspNetCore.Mvc;
using Readify.API.Filters;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.ReturnRequest;
using Readify.BLL.Features.ReturnRequest.DTOs;
using Readify.BLL.Features.ReturnRequest.Services;
using Readify.BLL.Specifications.ReturnRequestSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{
    public class ReturnRequestController : BaseController
    {
        private readonly IReturnRequestService _returnRequestService;

        public ReturnRequestController(IReturnRequestService returnRequestService)
        {
            _returnRequestService = returnRequestService;  
        }

        #region Create Return Request

        [RoleBasedAuthorization(UserType.User)]
        [HttpPost("CreateReturnRequest")]
        [SwaggerOperation(
            Summary = "Create a return request",
            Description = "Allows a user to create a return request for a borrowed book.")]
        [SwaggerResponse(201, "Return request created successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Failed to create return request", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(201, typeof(CreateReturnRequestSuccessExample))]
        [SwaggerResponseExample(400, typeof(CreateReturnRequestErrorExample))]
        public async Task<IActionResult> CreateReturnRequest([FromBody] CreateReturnRequestDto requestDto)
        {
            var errors = await _returnRequestService.CreateReturnRequestAsync(requestDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Failed to create return request", errors: errors));

            return CreatedAtAction(nameof(CreateReturnRequest), new ApiResponse<string>(201, "Return request created successfully"));
        }

        #endregion

        #region Get All Return Requests

        [HttpGet("GetAllReturnRequests")]
        [SwaggerOperation(
            Summary = "Retrieve all return requests",
            Description = "Fetches a paginated list of all return requests, with optional filters like status or date.")]
        [SwaggerResponse(200, "List of return requests retrieved successfully", typeof(ApiResponse<ListReturnRequestsDto>))]
        [SwaggerResponseExample(200, typeof(GetAllReturnRequestsSuccessExample))]
        public async Task<IActionResult> GetAllReturnRequests([FromQuery] ReturnRequestSpecification specs)
        {
            var result = await _returnRequestService.GetAllReturnRequestsAsync(specs);

            return Ok(new ApiResponse<ListReturnRequestsDto>(200, "List of return requests retrieved successfully", result));
        }

        #endregion

        #region Get Detailed Return Request by ID

        [HttpGet("GetReturnRequestById/{id}")]
        [SwaggerOperation(
            Summary = "Get detailed return request by ID",
            Description = "Retrieves a return request with detailed information including book and borrower data.")]
        [SwaggerResponse(200, "Return request found", typeof(ApiResponse<DetailedReturnRequestDto>))]
        [SwaggerResponse(404, "Return request not found", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(200, typeof(GetDetailedReturnRequestSuccessExample))]
        [SwaggerResponseExample(404, typeof(GetReturnRequestNotFoundExample))]
        public async Task<IActionResult> GetReturnRequestById(int id)
        {
            var result = await _returnRequestService.GetReturnRequestByIdAsync(id);

            if (result == null)
                return NotFound(new ApiResponse<string>(404, "Return request not found"));

            return Ok(new ApiResponse<DetailedReturnRequestDto>(200, "Return request found", result));
        }

        #endregion

        #region Delete Return Request by ID

        [HttpDelete("DeleteReturnRequest/{id}")]
        [SwaggerOperation(
            Summary = "Delete a return request by ID",
            Description = "Deletes a return request if it passes validation and exists in the system.")]
        [SwaggerResponse(200, "Return request deleted successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Validation errors occurred", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(DeleteReturnRequestSuccessExample))]
        [SwaggerResponseExample(400, typeof(DeleteReturnRequestErrorExample))]
        public async Task<IActionResult> DeleteReturnRequest(int id)
        {
            var errors = await _returnRequestService.DeleteRequestByIdAsync(id);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Validation errors occurred", errors: errors));

            return Ok(new ApiResponse<string>(200, "Return request deleted successfully"));
        }

        #endregion

        #region Update Return Request Status

        [RoleBasedAuthorization(UserType.Librarian)]
        [HttpPut("UpdateReturnRequestStatus")]
        [SwaggerOperation(
            Summary = "Update status of a return request",
            Description = "Allows Librarian to update the status an existing return request.")]
        [SwaggerResponse(200, "Return request updated successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Validation or update error", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(UpdateReturnRequestSuccessExample))]
        [SwaggerResponseExample(400, typeof(UpdateReturnRequestErrorExample))]
        public async Task<IActionResult> UpdateReturnRequestStatus(UpdateReturnRequestDto requestDto)
        {
            var errors = await _returnRequestService.UpdateReturnRequestStatusAsync(requestDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Validation or update error", errors: errors));

            return Ok(new ApiResponse<string>(200, "Return request updated successfully"));
        }

        #endregion

    }
}
