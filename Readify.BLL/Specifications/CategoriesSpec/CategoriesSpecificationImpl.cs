using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.CategoriesSpec
{
    internal class CategoriesSpecificationImpl : BaseSpecification<Category>
    {
        public CategoriesSpecificationImpl(CategoriesSpecification specification) :
          base(c =>
         (!specification.SearchById.HasValue || c.Id == specification.SearchById.Value) &&
         (string.IsNullOrEmpty(specification.SearchByName) || c.Name.ToString() == specification.SearchByName)
          )
        {

            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            AddInclude(b => b.BookCategories);

            ApplyNoTracking();
        }
    }
}