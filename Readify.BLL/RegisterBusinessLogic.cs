namespace Readify.BLL
{
    public static class RegisterBusinessLogic
    {
        public static IServiceCollection RegisterBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IBookCategoriesService, BookCategoriesService>();
            services.AddScoped<IBorrowRequestService, BorrowRequestService>();
            services.AddScoped<IBorrowedBookService, BorrowedBookService>();

            // validators
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IBookValidator, BookValidator>();
            services.AddScoped<IBorrowRequestValidator, BorrowRequestValidator>();
            services.AddScoped<IBorrowedBookValidator, BorrowedBookValidator>();

            return services;
        }
    }
}