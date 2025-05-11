using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.UserBorrowedBooksSpec
{
    public class UserBorrowedBooksSpecificationImpl : BaseSpecification<BorrowedBook>
    {
        public UserBorrowedBooksSpecificationImpl(string userId, UserBorrowedBooksSpecification specification) : base(bb => bb.UserId == userId)
        {
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            AddInclude(b => b.User);
            AddInclude(b => b.Book);

            ApplyNoTracking();
        }
    }
}
