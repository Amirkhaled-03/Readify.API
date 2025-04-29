using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.BorrowedBookSpec
{
    internal class BorrowedBookSpecificationImpl : BaseSpecification<BorrowedBook>
    {
        public BorrowedBookSpecificationImpl(BorrowedBookSpecification specification)
            : base(bb =>
                (!specification.SearchById.HasValue || bb.Id == specification.SearchById.Value) &&
                (!specification.SearchByBookId.HasValue || bb.BookId == specification.SearchByBookId.Value) &&
                (string.IsNullOrEmpty(specification.SearchByUserId) || bb.UserId.ToLower() == specification.SearchByUserId) &&
                (string.IsNullOrEmpty(specification.SearchByConfirmedById) || bb.ConfirmedBy.ToLower() == specification.SearchByConfirmedById) &&
                (!specification.SearchByBorrowedAt.HasValue || bb.BorrowedAt.Date == specification.SearchByBorrowedAt.Value.Date) &&
                (!specification.SearchByDueDate.HasValue || bb.DueDate.Date == specification.SearchByDueDate.Value.Date) &&
                (!specification.SearchByReturnedAt.HasValue || (bb.ReturnedAt.HasValue && bb.ReturnedAt.Value.Date == specification.SearchByReturnedAt.Value.Date) &&
                (!specification.SearchByStatus.HasValue || bb.Status == specification.SearchByStatus.Value))
            )
        {
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            AddInclude(b => b.User);
            AddInclude(b => b.ConfirmedBy);
            AddInclude(b => b.Book);

            ApplyNoTracking();
        }
    }
}
