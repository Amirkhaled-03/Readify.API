using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Validators.BorrowRequestValidators
{
    internal interface IBorrowRequestValidator
    {
        Task<List<string>> ValidateCreateBorrowRequest(string userid, int bookId, DateTime startDate, DateTime endDate);
        Task<List<string>> ValidateDeleteBorrowRequest(int id);
        Task<List<string>> ValidateUpdateBorrowRequest(int id, BorrowRequestStatus status);
    }
}
