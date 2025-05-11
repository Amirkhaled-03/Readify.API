using System.Linq.Expressions;

namespace Readify.DAL.SpecificationPattern
{
    public interface IBaseSpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderByAscending { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
        bool IsTracking { get; }

        // ADDEDD
        List<string> IncludeStrings { get; }
        /////////////////////////////////////////////////
    }
}
