﻿using Readify.BLL.Features.Chat.Services;
using Readify.BLL.Features.Librarian.Services;
using Readify.BLL.Features.ReturnRequest.Services;
using Readify.BLL.Features.User.Services;
using Readify.BLL.ServiceContracts.AccountContracts;
using Readify.BLL.Validators.ReturnRequestValidator;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILibrarianService, LibrarianService>();
            services.AddScoped<IBookCategoriesService, BookCategoriesService>();
            services.AddScoped<IBorrowRequestService, BorrowRequestService>();
            services.AddScoped<IBorrowedBookService, BorrowedBookService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IReturnRequestService, ReturnRequestService>();
            services.AddScoped<IChatService, ChatService>();


            // validators
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IBookValidator, BookValidator>();
            services.AddScoped<IBorrowRequestValidator, BorrowRequestValidator>();
            services.AddScoped<IBorrowedBookValidator, BorrowedBookValidator>();
            services.AddScoped<IReturnRequestValidator, ReturnRequestValidator>();


            return services;
        }
    }
}