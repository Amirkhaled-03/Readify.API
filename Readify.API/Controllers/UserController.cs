using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.User;
using Readify.BLL.Features.Librarian.DTOs;
using Readify.BLL.Features.User.DTOs;
using Readify.BLL.Features.User.Services;
using Readify.BLL.Specifications.UserSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using static Readify.API.ResponseExample.User.ManageUsersPageExample;

namespace Readify.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        #region Get All Users

        [HttpGet]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ManageLibrarianPageDto>))]
        [SwaggerResponseExample(200, typeof(ManageUsersPageExample))]
        [SwaggerOperation(
            Summary = "Get all Users",
            Description = "This endpoint retrieves a paginated list of users. Supports filtering by Username, Fullname, PhoneNumber, Status, and ID."
        )]
        public async Task<IActionResult> GetAll([FromQuery] UserSpecification specification)
        {
            var result = await _userService.GetUsersAsync(specification);

            return Ok(new ApiResponse<ManageUsersPageDto>(200, "success", result));
        }

        #endregion

        #region Get User By Id

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<LibrarianDto>))]
        [SwaggerResponse(404, "Not Found", typeof(ApiResponse<object>))]
        [SwaggerResponseExample(200, typeof(GetUserByIdSuccessExample))]
        [SwaggerResponseExample(404, typeof(GetUserByIdNotFoundExample))]
        [SwaggerOperation(
            Summary = "Get User by ID",
            Description = "Retrieves detailed information about a specific user by their unique ID."
        )]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound(new ApiResponse<object>(404, "User not found."));

            return Ok(new ApiResponse<UserDto>(200, "success", user));
        }

        #endregion

        #region Edit user 

        [HttpPut("Edit")]
        [SwaggerResponse(200, "User Updated", typeof(ApiResponse<List<string>>))]
        [SwaggerResponse(400, "Failed to Update User", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(EditUserSuccessExample))]
        [SwaggerResponseExample(400, typeof(EditUserFailedExample))]
        [SwaggerOperation(
        Summary = "Edit User",
        Description = "Allows editing user info (Fullname, Username, PhoneNumber) with validation for uniqueness."
        )]
        public async Task<IActionResult> EditUser([FromBody] EditUserDto editUser)
        {
            var errors = await _userService.EditUser(editUser);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Failed to update user", errors));

            return Ok(new ApiResponse<List<string>>(200, "User updated successfully", new List<string>()));
        }

        #endregion

        #region Delete User

        [HttpDelete("Delete/{id}")]
        [SwaggerResponse(200, "User Deleted", typeof(ApiResponse<List<string>>))]
        [SwaggerResponse(400, "Failed to Delete User", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(DeleteUserSuccessExample))]
        [SwaggerResponseExample(400, typeof(DeleteUserFailedExample))]
        [SwaggerOperation(
            Summary = "Delete a User by ID",
            Description = "Deletes a user by ID. If the user is not found, returns a failed message."
        )]
        public async Task<IActionResult> DeleteById(string id)
        {
            var result = await _userService.DeleteUserById(id);

            if (!result)
                return BadRequest(new ApiResponse<List<string>>(400, "Failed to delete user", new List<string> { "This user does not exist." }));

            return Ok(new ApiResponse<List<string>>(200, "User deleted successfully", new List<string>()));
        }

        #endregion

    }
}
