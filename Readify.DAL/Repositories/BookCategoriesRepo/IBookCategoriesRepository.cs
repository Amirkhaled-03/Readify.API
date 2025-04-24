using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.BookCategoriesRepo
{
    public interface IBookCategoriesRepository : IGenericRepository<BookCategory>
    {
        Task<IReadOnlyList<Category>> GetBookCategoriesByBookId(int Bookid);
        Task<IReadOnlyList<int>> GetBookCategoriesIdsByBookId(int Bookid);
    }
}
