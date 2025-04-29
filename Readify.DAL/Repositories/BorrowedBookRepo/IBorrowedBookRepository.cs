using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.BorrowedBookRepo
{
    public interface IBorrowedBookRepository : IGenericRepository<BorrowedBook>
    {
        Task<BorrowedBook?> GetBorrowedBookByIdAsync(int id);
        Task<IEnumerable<BorrowedBook>> GetUserBorrowedBooksAsync(string userId);
    }
}
