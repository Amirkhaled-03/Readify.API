using Readify.DAL.Repositories.BookCategoriesRepo;
using Readify.DAL.Repositories.BookRepo;
using Readify.DAL.Repositories.BorrowedBookRepo;
using Readify.DAL.Repositories.BorrowRequestRepo;
using Readify.DAL.Repositories.CategoriesRepo;
using Readify.DAL.Repositories.ChatRepo;
using Readify.DAL.Repositories.MessageRepo;
using Readify.DAL.Repositories.ReturnRequestRepo;

namespace Readify.DAL.UOW
{
    public interface IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
        public IBorrowedBookRepository BorrowedBookRepository { get; }
        public IBorrowRequestRepository BorrowRequestRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }
        public IBookCategoriesRepository BookCategoriesRepository { get; }
        public IReturnRequestRepository ReturnRequestRepository { get; }
        public IConversationRepository ConversationRepository { get; }
        public IMessageRepository MessageRepository { get; }

        Task<int> SaveAsync();
    }
}