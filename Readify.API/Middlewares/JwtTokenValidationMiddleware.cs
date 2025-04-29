using Microsoft.AspNetCore.Identity;
using Readify.API.HandleResponses;
using Readify.DAL.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Readify.API.Middlewares
{
    public class JwtTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtTokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var path = context.Request.Path.Value;
            var accountController = "/api/Account";

            if (path.Equals($"{accountController}/Login", StringComparison.OrdinalIgnoreCase) ||
                path.Equals($"{accountController}/RegisterAdmin", StringComparison.OrdinalIgnoreCase) ||
                path.Equals($"{accountController}/RegisterLibrarian", StringComparison.OrdinalIgnoreCase) ||
                path.Equals($"{accountController}/RegisterUser", StringComparison.OrdinalIgnoreCase)
                )
            {
                await _next(context);
                return;
            }

            // Check if Authorization header exists
            if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader) || string.IsNullOrEmpty(authHeader))
            {
                await ReturnUnauthorizedResponse(context, "Invalid Token");
                return;
            }

            var token = authHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                ? authHeader.ToString().Substring("Bearer ".Length).Trim()
                : authHeader.ToString();

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Check token expiration
                if (jwtToken.Payload.Exp.HasValue)
                {
                    var expirationTime = DateTimeOffset.FromUnixTimeSeconds(jwtToken.Payload.Exp.Value);
                    if (expirationTime < DateTime.UtcNow)
                    {
                        await ReturnUnauthorizedResponse(context, "Token has expired.");
                        return;
                    }
                }

                // Validate user ID
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    await ReturnUnauthorizedResponse(context, "هذا المستخدم غير موجود");
                    return;
                }

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    await ReturnUnauthorizedResponse(context, "هذا المستخدم غير موجود");
                    return;
                }
            }
            catch
            {
                await ReturnServerErrorResponse(context, "An unexpected error occurred. | حدث خطأ غير متوقع.");
                return;
            }

            await _next(context); // Proceed if all checks pass
        }

        private async Task ReturnUnauthorizedResponse(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<string>(
                statusCode: 401,
                message: "Unauthorized",
                data: message
            );

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private async Task ReturnServerErrorResponse(HttpContext context, string errorMessage)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<string>(500, "Server Error", errors: new List<string> { errorMessage });
            await context.Response.WriteAsJsonAsync(response);
        }
    }

}
