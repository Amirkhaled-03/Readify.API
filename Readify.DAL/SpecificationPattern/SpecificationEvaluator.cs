using Microsoft.EntityFrameworkCore;
using Readify.DAL.Entities;

namespace Readify.DAL.SpecificationPattern
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        // IQueryable=> wait till execute on server
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IBaseSpecification<T> specification)
        {
            var query = inputQuery;

            // Apply filtering criteria
            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            // Apply ordering
            if (specification.OrderByAscending is not null)
                query = query.OrderBy(specification.OrderByAscending);

            if (specification.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);

            // Apply pagination
            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);

            // Apply includes for eager loading   // accumelator  current = query in first time
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Apply No Tracking if specified
            if (!specification.IsTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }
    }

}
