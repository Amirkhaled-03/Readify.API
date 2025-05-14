using Readify.DAL.Entities.Identity;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.AccountSpec
{
    internal class AcceptAccountsSpecificationImpl : BaseSpecification<ApplicationUser>
    {
        public AcceptAccountsSpecificationImpl(AcceptAccountsSpec specification)
           : base(u => u.UserType == UserType.Librarian &&
                (string.IsNullOrEmpty(specification.SearchByFullname) || u.Fullname.ToLower().Contains(specification.SearchByFullname)) &&
                (!specification.Status.HasValue ? u.UserStatus == UserStatus.Pending || u.UserStatus == UserStatus.Rejected
                                                : u.UserStatus == specification.Status.Value) 
                )
        {
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            ApplyNoTracking();

            AddInclude(u => u.BorrowedBooks);

            AddOrderByDescending(u => u.CreatedAt);
        }
    }
}