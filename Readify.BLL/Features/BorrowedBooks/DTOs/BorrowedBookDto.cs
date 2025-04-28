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

        public string UserId { get; set; }
        public int BookId { get; set; }

        public DateTime BorrowedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; } // if it has value, this means that the book is returned

        public string? ConfirmedById { get; set; }

    }
}
