using Microsoft.EntityFrameworkCore;
using Readify.DAL.DBContext;
using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.BookRepo
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<Book?> GetDetailedBookById(int id)
        {
            return await _dbSet.Where(b => b.Id == id)
                  .AsNoTracking()
                  .Include(b => b.BookCategories)
                  .ThenInclude(c => c.Category)
                  .FirstOrDefaultAsync();
        }
    }
}