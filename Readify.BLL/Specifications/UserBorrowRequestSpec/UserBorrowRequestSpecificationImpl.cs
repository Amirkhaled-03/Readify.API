using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.UserBorrowRequestSpec
{
    public class UserBorrowRequestSpecificationImpl : BaseSpecification<BorrowRequest>
    {
        public UserBorrowRequestSpecificationImpl(string userId,UserBorrowRequestSpecification specification) : base(br => br.UserId == userId)
        {
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            AddInclude(b => b.User);
            AddInclude(b => b.Book);

            ApplyNoTracking();
        }
    }
}
