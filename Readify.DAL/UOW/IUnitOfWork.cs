using Readify.DAL.Repositories.BookCategoriesRepo;
using Readify.DAL.Repositories.BookRepo;
using Readify.DAL.Repositories.BorrowedBookRepo;
using Readify.DAL.Repositories.BorrowRequestRepo;
using Readify.DAL.Repositories.CategoriesRepo;
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

        Task<int> SaveAsync();
    }
}