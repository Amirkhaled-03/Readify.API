using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;
using System.Linq.Expressions;

namespace Readify.DAL.RepositoriesContracts
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAllAsQueryable();
        Task<IReadOnlyList<TEntity>> GetAllAsyncEnhanced();
        TEntity GetByID(int id);

        void AddEntity(TEntity entity);
        Task<List<T>> GetSpecificColumnsAsync<T>(Expression<Func<TEntity, T>> selector);

        bool DeleteEntity(int id);
        bool DeleteEntity(TEntity entity);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        void AddRange(IEnumerable<TEntity> entities);

        void UpdateEntity(TEntity entity);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
        Task<T> GetSpecificColumnFirstOrDefaultAsync<T>(Expression<Func<TEntity, T>> selector);
        Task<TEntity> GetEntityWithSpecificationsAsync(IBaseSpecification<TEntity> specifications);

        Task<int> CountWithSpecAsync(IBaseSpecification<TEntity> specifications);
        Task<IEnumerable<TEntity>> GetWithSpecificationsAsync(IBaseSpecification<TEntity> specifications);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
