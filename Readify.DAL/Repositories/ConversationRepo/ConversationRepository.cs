using Microsoft.EntityFrameworkCore;
using Readify.DAL.DBContext;
using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.ChatRepo
{
    public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<Conversation?> GetByUserAsync(string userId)
        {
            return await _dbSet.Where(c => c.UserId == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

    }
}
