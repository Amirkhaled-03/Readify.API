using System.Linq.Expressions;

namespace Readify.DAL.SpecificationPattern
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria ?? (e => true);
            Includes = new List<Expression<Func<T, object>>>();
            IsTracking = true;
        }

        public BaseSpecification() : this(e => true) { }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public Expression<Func<T, object>> OrderByAscending { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginated { get; private set; }
        public bool IsTracking { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);

        public BaseSpecification<T> AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderByAscending = orderBy;
            return this;
        }

        public BaseSpecification<T> AddOrderByDescending(Expression<Func<T, object>> orderBy)
        {
            OrderByDescending = orderBy;
            return this;
        }

        public BaseSpecification<T> ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginated = true;
            return this;
        }

        public BaseSpecification<T> IgnorePagination()
        {
            IsPaginated = false;
            Skip = 0;
            Take = 0;
            return this;
        }

        public BaseSpecification<T> ApplyNoTracking()
        {
            IsTracking = false;
            return this;
        }

        // ADDEDD
        public List<string> IncludeStrings { get; } = new List<string>();
        protected void AddInclude(string includeString) => IncludeStrings.Add(includeString);
    }
}
