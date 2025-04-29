using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Validators.BorrowedBookValidators
{
    public interface IBorrowedBookValidator
    {
        Task<List<string>> ValidateUpdateBorrowedBook(int id, BorrowedBookStatus status);
    }
}
