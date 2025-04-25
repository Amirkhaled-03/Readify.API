using Microsoft.EntityFrameworkCore;
using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.BorrowRequestRepo
{
    public interface IBorrowRequestRepository : IGenericRepository<BorrowRequest>
    {
        Task<BorrowRequest?> GetRequestById(int id);
        Task<IEnumerable<BorrowRequest>> GetUserRequestsIncludesAsync(string userId);
    }
}
