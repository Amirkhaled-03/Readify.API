using Readify.DAL.DBContext;
using Readify.DAL.Repositories.BookRepo;
using Readify.DAL.Repositories.BorrowedBookRepo;
using Readify.DAL.Repositories.BorrowRequestRepo;
using Readify.DAL.Repositories.CategoriesRepo;

namespace Readify.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dBContext;
        public IBookRepository BookRepository { get; }
        public IBorrowedBookRepository BorrowedBookRepository { get; }
        public IBorrowRequestRepository BorrowRequestRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }

        public UnitOfWork(
            ApplicationDBContext dBContext,
            IBookRepository bookRepository,
            IBorrowedBookRepository borrowedBookRepository,
            IBorrowRequestRepository borrowRequestRepository,
            ICategoriesRepository categoriesRepository
            )
        {
            _dBContext = dBContext;
            BookRepository = bookRepository;
            BorrowedBookRepository = borrowedBookRepository;
            BorrowRequestRepository = borrowRequestRepository;
            CategoriesRepository = categoriesRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }
    }
}