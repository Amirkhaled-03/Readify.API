namespace Readify.DAL.UOW
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
