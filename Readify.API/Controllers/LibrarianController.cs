using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.Librarian.Readify.API.ResponseExample.Librarian;
using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Features.Librarian.Services;
using Readify.BLL.Specifications.LibrarianSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{
    public class LibrarianController : BaseController
    {
        private readonly ILibrarianService _librarianService;

        public LibrarianController(ILibrarianService librarianService)
        {
            _librarianService = librarianService;
        }

        #region Get All Librarians

        [HttpGet]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ManageLibrarianPageDto>))]
        [SwaggerResponseExample(200, typeof(ManageLibrarianPageExample))]
        [SwaggerOperation(
            Summary = "Get all Librarians",
            Description = "This endpoint allows Admins to retrieve a paginated list of librarians. You can filter results using query parameters such as Username, Fullname, Phone, Status, or ID."
        )]
        public async Task<IActionResult> GetAll([FromQuery] LibrarianSpecifications specification)
        {
            var result = await _librarianService.GetLibrarianAsync(specification);

            return Ok(new ApiResponse<ManageLibrarianPageDto>(200, "success", result));
        }

        #endregion

        #region Get Librarian By Id

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<LibrarianDto>))]
        [SwaggerResponse(404, "Not Found", typeof(ApiResponse<object>))]
        [SwaggerResponseExample(200, typeof(GetLibrarianByIdSuccessExample))]
        [SwaggerResponseExample(404, typeof(GetLibrarianByIdNotFoundExample))]
        [SwaggerOperation(
            Summary = "Get Librarian by ID",
            Description = "Retrieves detailed information about a specific librarian by their unique ID."
        )]
        public async Task<IActionResult> GetById(string id)
        {
            var librarian = await _librarianService.GetLibrarianByIdAsync(id);

            if (librarian == null)
                return NotFound(new ApiResponse<object>(404, "Librarian not found."));

            return Ok(new ApiResponse<LibrarianDto>(200, "success", librarian));
        }

        #endregion

        #region Edit Librarian

        [HttpPut("Edit")]
        [SwaggerResponse(200, "Librarian Updated", typeof(ApiResponse<List<string>>))]
        [SwaggerResponse(400, "Failed to Update Librarian", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(EditLibrarianSuccessExample))]
        [SwaggerResponseExample(400, typeof(EditLibrarianFailedExample))]
        [SwaggerOperation(
            Summary = "Edit Librarian",
            Description = "This endpoint allows updating librarian information (Fullname, Username, PhoneNumber) with validation for uniqueness."
        )]
        public async Task<IActionResult> EditLibrarian([FromBody] EditLibrarianDto editLibrarian)
        {
            var errors = await _librarianService.EditLibrarian(editLibrarian);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Failed to update librarian", errors));

            return Ok(new ApiResponse<List<string>>(200, "Librarian updated successfully", new List<string>()));
        }

        #endregion

        #region Delete Librarian

        [HttpDelete("Delete/{id}")]
        [SwaggerResponse(200, "Librarian Deleted", typeof(ApiResponse<List<string>>))]
        [SwaggerResponse(400, "Failed to Delete Librarian", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(DeleteLibrarianSuccessExample))]
        [SwaggerResponseExample(400, typeof(DeleteLibrarianFailedExample))]
        [SwaggerOperation(
            Summary = "Delete a Librarian by ID",
            Description = "Deletes a librarian by ID. If the librarian is not found, returns a failed message."
        )]
        public async Task<IActionResult> DeleteById(string id)
        {
            var result = await _librarianService.DeleteLibrarianById(id);

            if (!result)
                return BadRequest(new ApiResponse<List<string>>(400, "Failed to delete librarian", new List<string> { "This librarian does not exist." }));

            return Ok(new ApiResponse<List<string>>(200, "Librarian deleted successfully", new List<string>()));
        }

        #endregion

    }
}