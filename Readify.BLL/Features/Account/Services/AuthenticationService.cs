using Microsoft.AspNetCore.Identity;
using Readify.BLL.Features.Account.DTOs.Login;
using Readify.DAL.Entities.Identity;

namespace Readify.BLL.Features.Account.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ITokenService tokenService
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        public async Task<LoginResultDto> LogIn(LogInDto logInDto)
        {
            var loginResult = new LoginResultDto();

            // Find user by email
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == logInDto.Email);

            if (user == null)
            {
                loginResult.Errors.Add("Invalid email or password.");
                return loginResult;
            }

            // Validate password
            bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, logInDto.Password);
            if (!isCorrectPassword)
            {
                loginResult.Errors.Add("Invalid email or password.");
                return loginResult;
            }

            // Sign in the user
            var signInResult = await _signInManager.PasswordSignInAsync(user, logInDto.Password, false, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                loginResult.Errors.Add("An error occurred during login. Please try again later.");
                return loginResult;
            }

            // Generate token and return success
            loginResult.Token = _tokenService.CreateToken(user);

            return loginResult;
        }

    }
}
