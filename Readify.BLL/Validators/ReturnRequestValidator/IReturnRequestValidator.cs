using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Validators.ReturnRequestValidator
{
    public interface IReturnRequestValidator
    {
        Task<List<string>> ValidateCreateReturnRequest(int borrowedBookId, DateTime returnDate);
        Task<List<string>> ValidateDeleteReturnRequest(int id);
        Task<List<string>> ValidateUpdateReturnRequest(int id, ReturnRequestStatus status);

    }
}
