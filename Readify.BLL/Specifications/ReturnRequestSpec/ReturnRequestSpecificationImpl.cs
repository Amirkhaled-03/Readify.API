using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.ReturnRequestSpec
{
    internal class ReturnRequestSpecificationImpl : BaseSpecification<ReturnRequest>
    {
        public ReturnRequestSpecificationImpl(ReturnRequestSpecification specification)
          : base(rr =>
                (string.IsNullOrEmpty(specification.SearchByUserId) || rr.BorrowedBook.UserId.ToString() == specification.SearchByUserId) &&
                (string.IsNullOrEmpty(specification.SearchByApprovedBy) || rr.ApprovedBy.ToString() == specification.SearchByApprovedBy) &&
              (!specification.SearchByReturnDate.HasValue || rr.ReturnDate.Date == specification.SearchByReturnDate.Value.Date) &&
              (!specification.SearchByBookId.HasValue || rr.BorrowedBook.BookId == specification.SearchByBookId.Value) &&
              (!specification.SearchByBorrowedBookId.HasValue || rr.BorrowedBookId == specification.SearchByBorrowedBookId.Value) &&
              (!specification.SearchByStatus.HasValue || rr.Status == specification.SearchByStatus.Value) &&
              (!specification.SearchById.HasValue || rr.Id == specification.SearchById.Value)

          )
        {
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            AddInclude(rr => rr.BorrowedBook);
            AddInclude(rr => rr.BorrowedBook.Book);
            AddInclude(rr => rr.BorrowedBook.User);

            ApplyNoTracking();

            AddOrderByDescending(rr => rr.CreatedAt);
        }

    }
}
