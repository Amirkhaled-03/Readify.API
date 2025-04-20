using Readify.DAL.DBContext;

namespace Readify.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dBContext;

        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<int> SaveAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }
    }
}
