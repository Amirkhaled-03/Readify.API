using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Readify.API.HandleResponses;
using Readify.BLL.Features.JWTToken;

namespace Readify.API.Filters
{
    public class RoleBasedAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly UserType[] _allowedRoles;

        public RoleBasedAuthorizationAttribute(params UserType[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenService = context.HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService;

            if (tokenService == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var roleClaim = tokenService.GetUserRoleFromToken();

            if (string.IsNullOrEmpty(roleClaim) || !Enum.TryParse<UserType>(roleClaim, out var userRole))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!_allowedRoles.Contains(userRole))
            {
                context.Result = new ObjectResult(
                    new ApiResponse<string>(
                        403,
                        "You do not have the authority to perform this action.",
                        null
                    )
                )
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}