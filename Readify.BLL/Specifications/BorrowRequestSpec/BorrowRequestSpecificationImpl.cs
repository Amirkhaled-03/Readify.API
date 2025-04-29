using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.BorrowRequestSpec
{
    internal class BorrowRequestSpecificationImpl : BaseSpecification<BorrowRequest>
    {
        public BorrowRequestSpecificationImpl(BorrowRequestSpecification specification)
            : base(br =>
                (string.IsNullOrEmpty(specification.SearchByUserId) || br.UserId.ToString() == specification.SearchByUserId) &&
                (!specification.SearchByBookId.HasValue || br.BookId == specification.SearchByBookId.Value) &&
                (!specification.SearchByStatus.HasValue || br.Status == specification.SearchByStatus.Value)
            )
        {
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            AddInclude(br => br.Book);
            AddInclude(br => br.User);
            AddInclude(br => br.ApprovedBy);

            ApplyNoTracking();

            AddOrderByDescending(br => br.RequestedAt);
        }
    }
}