using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Features.ReturnRequest.DTOs
{
    public class CreateReturnRequestDto
    {
        public int BorrowedBookId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
