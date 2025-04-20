using Microsoft.EntityFrameworkCore;
using Readify.DAL.DBContext;
using Readify.DAL.Entities;
using Readify.DAL.RepositoriesContracts;
using Readify.DAL.SpecificationPattern;
using System.Linq.Expressions;

namespace Readify.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync()
          => await _dbSet.ToListAsync();
        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsyncEnhanced()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual IQueryable<TEntity> GetAllAsQueryable()
        {
            return _dbSet.AsNoTracking();
        }

        public TEntity GetByID(int id)
          => _dbSet.Find(id);

        public void AddEntity(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual bool DeleteEntity(int id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);

            return true;
        }

        public virtual bool DeleteEntity(TEntity entity)
        {

            _dbSet.Remove(entity);

            return true;
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }
        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<int> CountAsync()
          => await _dbSet.CountAsync();
        public async Task<T> GetSpecificColumnFirstOrDefaultAsync<T>(Expression<Func<TEntity, T>> selector)
        {
            return await _dbSet.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetEntityWithSpecificationsAsync(IBaseSpecification<TEntity> specifications)
             => await ApplySpecifications(specifications).FirstOrDefaultAsync();

        public async Task<List<T>> GetSpecificColumnsAsync<T>(Expression<Func<TEntity, T>> selector)
        {
            return await _dbSet.Select(selector).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetWithSpecificationsAsync(IBaseSpecification<TEntity> specifications)
            => await ApplySpecifications(specifications).ToListAsync();

        public async Task<int> CountWithSpecAsync(IBaseSpecification<TEntity> specifications)
            => await ApplySpecifications(specifications).CountAsync();

        private IQueryable<TEntity> ApplySpecifications(IBaseSpecification<TEntity> specifications)
            => SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specifications);


    }
}
