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
    }
}