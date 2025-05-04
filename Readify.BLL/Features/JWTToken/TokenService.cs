using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Readify.DAL.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Readify.BLL.Features.JWTToken
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(ApplicationUser appUser)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Id", appUser.Id ?? string.Empty),
                new Claim("role", appUser.UserType.ToString() ?? string.Empty),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetUserIdFromToken()
        {
            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return null; // No token provided
            }

            // Extract token
            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Retrieve the user ID from the token
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            return userId;
        }

        public string GetUserRoleFromToken()
        {
            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return null; // No token provided
            }

            // Extract token
            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Retrieve the user role from the token
            var userRole = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            return userRole;
        }

        public async Task<ApplicationUser> GetUserFromTokenAsync()
        {
            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return null; // No token provided
            }

            // Extract token
            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Retrieve the user ID from the token
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }
    }
}
