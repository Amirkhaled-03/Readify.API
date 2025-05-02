using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Features.ReturnRequest.DTOs
{
    public class DetailedReturnRequestDto
    {
        public int Id { get; set; }
        public int BorrowedBookId { get; set; }
        public string BorrowedBookName { get; set; }
        public string BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? ApprovedBy { get; set; }
        public ReturnRequestStatus Status { get; set; }

    }
}
