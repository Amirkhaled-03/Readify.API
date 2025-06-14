﻿using Microsoft.AspNetCore.Mvc;
using Readify.API.Filters;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.Account;
using Readify.BLL.Features.Account.DTOs.AccountApproval;
using Readify.BLL.Features.Account.DTOs.Login;
using Readify.BLL.Features.Account.DTOs.Registration;
using Readify.BLL.Features.Account.ServicesContracts;
using Readify.BLL.ServiceContracts.AccountContracts;
using Readify.BLL.Specifications.AccountSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserManagementService _userManagementService;

        public AccountController(IAuthenticationService authenticationService, IUserRegistrationService userRegistrationService, IUserManagementService userManagementService)
        {
            _authenticationService = authenticationService;
            _userRegistrationService = userRegistrationService;
            _userManagementService = userManagementService;
        }

        #region Login 

        [HttpPost("Login")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<LoginResultDto>))]
        [SwaggerResponse(400, "validation Error", typeof(ApiResponse<LoginResultDto>))]
        [SwaggerResponse(403, "forbidden", typeof(ApiResponse<LoginResultDto>))]
        [SwaggerResponseExample(200, typeof(LoginSuccessResponseExample))]
        [SwaggerResponseExample(400, typeof(LoginErrorResponseExample))]
        [SwaggerResponseExample(403, typeof(LoginForbiddenResponseExample))]

        [SwaggerOperation(
        Summary = "Login the user",
        Description = "This endpoint allows users to Login")]
        public async Task<IActionResult> Login(LogInDto logInDto)
        {
            var res = await _authenticationService.LogIn(logInDto);

            if (res.LoginCode == 1)
                return StatusCode(403, new ApiResponse<IReadOnlyList<string>>(400, "forbidden", errors: res.Errors));

            if (res.LoginCode == 2)
                return StatusCode(400, new ApiResponse<IReadOnlyList<string>>(400, "bad request", errors: res.Errors));

            else
                return Ok(new ApiResponse<LoginResultDto>(200, "success", data: res));
        }

        #endregion

        #region Create Admin Account 

        [RoleBasedAuthorization(UserType.Admin)]
        [HttpPost("RegisterAdmin")]
        [SwaggerOperation(
         Summary = "Create Admin",
         Description = "This endpoint allows Create Admin")]
        [SwaggerResponse(201, "Created successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "bad request", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(201, typeof(RegisterAdminSuccessExample))]
        [SwaggerResponseExample(400, typeof(RegisterAdminErrorExample))]
        public async Task<IActionResult> RegisterAdmin(RegisterApplicationUser adminDto)
        {
            var errors = await _userRegistrationService.RegisterAdminAsync(adminDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "bad request", errors: errors));

            return CreatedAtAction(nameof(RegisterAdmin), new ApiResponse<string>(201, "Created successfully"));
        }

        #endregion

        #region Create Librarian Account 

        [HttpPost("RegisterLibrarian")]
        [SwaggerOperation(
         Summary = "Allow Create Librarian",
         Description = "This endpoint allows to Create Librarian")]
        [SwaggerResponse(201, "Created successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "bad request", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(201, typeof(RegisterAdminSuccessExample))]
        [SwaggerResponseExample(400, typeof(RegisterAdminErrorExample))]
        public async Task<IActionResult> RegisterLibrarian(RegisterApplicationUser librarian)
        {
            var errors = await _userRegistrationService.RegisterLibrarianAsync(librarian);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "bad request", errors: errors));

            return CreatedAtAction(nameof(RegisterLibrarian), new ApiResponse<string>(201, "Created successfully"));
        }

        #endregion

        #region Create Librarian Account 

        [HttpPost("RegisterUser")]
        [SwaggerOperation(
         Summary = "Allow Admin to Create User",
         Description = "This endpoint allows Admins to Create User")]
        [SwaggerResponse(201, "Created successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "bad request", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(201, typeof(RegisterAdminSuccessExample))]
        [SwaggerResponseExample(400, typeof(RegisterAdminErrorExample))]
        public async Task<IActionResult> RegisterUser(RegisterApplicationUser userDto)
        {
            var errors = await _userRegistrationService.RegisterUserAsync(userDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "bad request", errors: errors));

            return CreatedAtAction(nameof(RegisterUser), new ApiResponse<string>(201, "Created successfully"));
        }

        #endregion

        #region Get Accounts Pending Approval

        [RoleBasedAuthorization(UserType.Admin)]
        //[RoleBasedAuthorization(UserType.Admin, UserType.Librarian)]
        [HttpGet("pending-accounts")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ManageAcceptsAccountsPageDto>))]
        [SwaggerResponseExample(200, typeof(GetPendingAccountsExample))]
        [SwaggerOperation(
            Summary = "Get accounts pending approval [userStatus 2 for accept, 3 for Rejected]",
            Description = "Returns a paginated list of users who are pending approval. Filterable by email, name, status, or type. [userStatus 2 for accept, 3 for Rejected]"
        )]
        public async Task<IActionResult> GetPendingAccounts([FromQuery] AcceptAccountsSpec spec)
        {
            var result = await _userManagementService.GetAccountsForApprovalAsync(spec);
            return Ok(new ApiResponse<ManageAcceptsAccountsPageDto>(200, "success", result));
        }

        #endregion

        #region Update Account Status

        [RoleBasedAuthorization(UserType.Admin)]
        [HttpPut("UpdateAccountStatus")]
        [SwaggerOperation(
            Summary = "Update the status of a user account",
            Description = "This endpoint allows Admins to update the status of a user account (e.g., Pending, Approved, Rejected, etc.).")]
        [SwaggerResponse(200, "Status updated successfully", typeof(ApiResponse<string>))]
        [SwaggerResponse(400, "Bad request", typeof(ApiResponse<List<string>>))]
        [SwaggerResponseExample(200, typeof(UpdateAccountStatusSuccessExample))]
        [SwaggerResponseExample(400, typeof(UpdateAccountStatusErrorExample))]
        public async Task<IActionResult> UpdateAccountStatus(UpdateAccountStatusDto updateStatusDto)
        {
            var errors = await _userManagementService.UpdateAccountStatusAsync(updateStatusDto);

            if (errors.Any())
                return BadRequest(new ApiResponse<List<string>>(400, "Bad request", errors: errors));

            return Ok(new ApiResponse<string>(200, "Status updated successfully"));
        }

        #endregion

    }
}