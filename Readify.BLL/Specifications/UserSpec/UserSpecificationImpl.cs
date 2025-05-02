using Readify.DAL.Entities.Identity;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.UserSpec
{
    internal class UserSpecificationImpl : BaseSpecification<ApplicationUser>
    {
        public UserSpecificationImpl(UserSpecification specification)
            : base(u =>
                (u.UserType == UserType.User) &&
                (string.IsNullOrEmpty(specification.SearchByUserId) || u.Id.ToLower() == specification.SearchByUserId) &&
                (string.IsNullOrEmpty(specification.SearchByEmail) || u.UserName.ToLower().Contains(specification.SearchByEmail)) &&
                (string.IsNullOrEmpty(specification.SearchByPhoneNumber) || u.PhoneNumber.ToLower().Contains(specification.SearchByPhoneNumber)) &&
                (string.IsNullOrEmpty(specification.SearchByFullName) || u.Fullname.ToLower().Contains(specification.SearchByFullName)) &&
                (!specification.SearchByStatus.HasValue || u.UserStatus == specification.SearchByStatus.Value)
            )
        {
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            ApplyNoTracking();

            AddInclude(u => u.BorrowedBooks);
        }
    }
}