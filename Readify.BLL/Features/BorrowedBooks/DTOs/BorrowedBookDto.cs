using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Features.BorrowedBooks.DTOs
{
    public class BorrowedBookDto
    {
        public int Id { get; set; }

        public string BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public string BorrowerPhoneNo { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string? ConfirmedById { get; set; }
        public string? ConfirmedByUser { get; set; }
        public BorrowedBookStatus Status { get; set; }
    }
}
