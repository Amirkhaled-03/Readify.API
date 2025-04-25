using Microsoft.EntityFrameworkCore;
using Readify.DAL.DBContext;
using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.BorrowRequestRepo
{
    public class BorrowRequestRepository : GenericRepository<BorrowRequest>, IBorrowRequestRepository
    {
        public BorrowRequestRepository(ApplicationDBContext context) : base(context)
        {

        }

        public async Task<BorrowRequest?> GetRequestById(int id)
        {
            return await _dbSet.Where(br => br.Id == id)
                 .AsNoTracking()
                 .Include(br => br.User)
                 .Include(br => br.ApprovedBy)
                 .Include(br => br.Book)
                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BorrowRequest>> GetUserRequestsIncludesAsync(string userId)
        {
            return await _dbSet
                .Where(br => br.UserId == userId)
                .Include(br => br.User)
                .Include(br => br.ApprovedBy)
                .Include(br => br.Book)
                .ToListAsync();
        }
    }
}