using Readify.DAL.Entities.Identity;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.LibrarianSpec
{
    public class LibrarianSpecificationsImpl : BaseSpecification<ApplicationUser>
    {
        public LibrarianSpecificationsImpl(LibrarianSpecifications specification)
        : base(x =>
            (x.UserType == UserType.Librarian) &&
            (string.IsNullOrEmpty(specification.SearchById) || x.Id.ToLower().Trim().Contains(specification.SearchById)) &&
            (!specification.Status.HasValue || x.UserStatus == specification.Status.Value) &&
            (string.IsNullOrEmpty(specification.SearchByEmail) || x.UserName.ToLower().Trim().Contains(specification.SearchByEmail)) &&
            (string.IsNullOrEmpty(specification.SearchByFullname) || x.Fullname.ToLower().Trim().Contains(specification.SearchByFullname)) &&
            (string.IsNullOrEmpty(specification.SearchByPhone) || x.PhoneNumber.ToLower().Trim().Contains(specification.SearchByPhone))
        )
        {
            // Apply pagination
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            // Sorting logic
            AddOrderBy(p => p.CreatedAt);

            ApplyNoTracking();
        }
    }
}
