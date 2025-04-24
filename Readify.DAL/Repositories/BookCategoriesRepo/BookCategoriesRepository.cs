using Microsoft.EntityFrameworkCore;
using Readify.DAL.DBContext;
using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.BookCategoriesRepo
{
    internal class BookCategoriesRepository : GenericRepository<BookCategory>, IBookCategoriesRepository
    {
        public BookCategoriesRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<int>> GetBookCategoriesIdsByBookId(int Bookid)
        {
            return await _dbSet
                .Where(bc => bc.BookId == Bookid)
                .Select(bc => bc.CategoryId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Category>> GetBookCategoriesByBookId(int bookId)
        {
            return await _dbSet
                .Where(bc => bc.BookId == bookId)
                .Select(bc => bc.Category)
                .ToListAsync();
        }

    }
}