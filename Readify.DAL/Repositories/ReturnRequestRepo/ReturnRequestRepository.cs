using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Readify.DAL.DBContext;
using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.ReturnRequestRepo
{
    public class ReturnRequestRepository : GenericRepository<ReturnRequest>, IReturnRequestRepository
    {
        public ReturnRequestRepository(ApplicationDBContext context) : base(context)
        {

        }

        public async Task<ReturnRequest?> GetByIdAsync(int id)
        {
            return await _dbSet.Where(rr => rr.Id == id)
                .AsNoTracking()
                .Include(rr => rr.BorrowedBook)
                    .ThenInclude(bb => bb.Book)
                .Include(rr => rr.BorrowedBook)
                    .ThenInclude(bb => bb.User)
                .FirstOrDefaultAsync();
        }
    }
}
