using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.BookSpec
{
    internal class BookSpecificationsWithCategoryFilter : BaseSpecification<Book>
    {
        public BookSpecificationsWithCategoryFilter(BookSpecifications specification) :
           base(b =>
          (!specification.SearchById.HasValue || b.Id == specification.SearchById.Value) &&
          (string.IsNullOrEmpty(specification.SearchByBookTitle) || b.Title.ToString() == specification.SearchByBookTitle) &&
          (string.IsNullOrEmpty(specification.SearchByBookAuthor) || b.Author.ToString() == specification.SearchByBookTitle) &&
          (string.IsNullOrEmpty(specification.SearchByBookISBN) || b.ISBN.ToString() == specification.SearchByBookISBN) &&
          (!specification.SearchByBookCategory.HasValue || b.BookCategories.Any(c => c.CategoryId == specification.SearchByBookCategory.Value))

           )
        {

            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            AddInclude(b => b.BookCategories);

            ApplyNoTracking();

            AddOrderByDescending(b => b.CreatedAt);

        }
    }
}