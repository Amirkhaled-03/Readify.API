using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.Account;
using Readify.BLL.Features.Account.DTOs.Login;
using Readify.BLL.Features.Account.DTOs.Registration;
using Readify.BLL.Features.Account.ServicesContracts;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRegistrationService _userRegistrationService;

        public AccountController(IAuthenticationService authenticationService, IUserRegistrationService userRegistrationService)
        {
            _authenticationService = authenticationService;
            _userRegistrationService = userRegistrationService;
        }

        #region Login 

        [HttpPost("Login")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<LoginResultDto>))]
        [SwaggerResponse(400, "validation Error", typeof(ApiResponse<LoginResultDto>))]
        [SwaggerResponseExample(200, typeof(LoginSuccessResponseExample))]
        [SwaggerResponseExample(400, typeof(LoginErrorResponseExample))]
        [SwaggerOperation(
        Summary = "Login the user",
        Description = "This endpoint allows users to Login")]
        public async Task<IActionResult> Login(LogInDto logInDto)
        {
            var res = await _authenticationService.LogIn(logInDto);

            if (res.Errors.Any())
                return StatusCode(400, new ApiResponse<IReadOnlyList<string>>(400, "bad request", errors: res.Errors));

            return Ok(new ApiResponse<LoginResultDto>(200, "success", data: res));
        }

        #endregion


        #region Create Admin Account 

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

    }
}
