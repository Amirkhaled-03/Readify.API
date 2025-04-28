using Readify.DAL.Entities.Identity;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.LibrarianSpec
{
    public class LibrarianSpecificationsTable : BaseSpecification<ApplicationUser>
    {
        public LibrarianSpecificationsTable(LibrarianSpecifications specification)
        : base(x =>
            (string.IsNullOrEmpty(specification.SearchById) || x.Id.ToLower().Trim().Contains(specification.SearchById)) &&
            (!specification.Status.HasValue || x.UserStatus == specification.Status.Value) &&
            (string.IsNullOrEmpty(specification.SearchByUserName) || x.UserName.ToLower().Trim().Contains(specification.SearchByUserName)) &&
            (string.IsNullOrEmpty(specification.SearchByFullname) || x.Email.ToLower().Trim().Contains(specification.SearchByFullname)) &&
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
