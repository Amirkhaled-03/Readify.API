using Microsoft.Extensions.DependencyInjection;
using Readify.BLL.Features.Account.Services;
using Readify.BLL.Features.Account.ServicesContracts;
using Readify.BLL.Features.JWTToken;
using Readify.BLL.Validators.Account;

namespace Readify.BLL
{
    public static class RegisterBusinessLogic
    {
        public static IServiceCollection RegisterBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();

            // validators
            services.AddScoped<IAccountValidator, AccountValidator>();

            return services;
        }
    }
}