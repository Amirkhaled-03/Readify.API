using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.BookRepo
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetDetailedBookById(int id);
        Task<IEnumerable<Book>?> GetBookByCategory(int? categoryId);
    }
}