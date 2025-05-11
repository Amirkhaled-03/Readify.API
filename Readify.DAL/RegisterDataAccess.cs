using Microsoft.Extensions.DependencyInjection;
using Readify.DAL.Repositories.BookCategoriesRepo;
using Readify.DAL.Repositories.BookRepo;
using Readify.DAL.Repositories.BorrowedBookRepo;
using Readify.DAL.Repositories.BorrowRequestRepo;
using Readify.DAL.Repositories.CategoriesRepo;
using Readify.DAL.Repositories.ChatRepo;
using Readify.DAL.Repositories.MessageRepo;
using Readify.DAL.Repositories.ReturnRequestRepo;
using Readify.DAL.UOW;

namespace Readify.DAL
{
    public static class RegisterDataAccess
    {
        public static IServiceCollection RegisterDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();
            services.AddScoped<IBorrowRequestRepository, BorrowRequestRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IBookCategoriesRepository, BookCategoriesRepository>();
            services.AddScoped<IBorrowRequestRepository, BorrowRequestRepository>();
            services.AddScoped<IReturnRequestRepository, ReturnRequestRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();


            return services;
        }
    }
}