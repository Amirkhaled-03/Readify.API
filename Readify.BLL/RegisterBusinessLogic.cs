using Microsoft.Extensions.DependencyInjection;
using Readify.BLL.Features.Account.Services;
using Readify.BLL.Features.Account.ServicesContracts;
using Readify.BLL.Features.Admin.Services;
using Readify.BLL.Features.Book.Services;
using Readify.BLL.Features.BookCategories.Services;
using Readify.BLL.Features.BorrowedBooks.Services;
using Readify.BLL.Features.BorrowRequest.Services;
using Readify.BLL.Features.JWTToken;
using Readify.BLL.Validators.Account;
using Readify.BLL.Validators.BookValidator;
using Readify.BLL.Validators.BorrowedBookValidators;
using Readify.BLL.Validators.BorrowRequestValidators;

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