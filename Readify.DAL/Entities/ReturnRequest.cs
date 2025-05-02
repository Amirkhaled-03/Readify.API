using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.DAL.Entities
{
    public class ReturnRequest :BaseEntity
    {
        public int Id { get; set; }
        public int BorrowedBookId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? ApprovedBy { get; set; }
        public ReturnRequestStatus Status { get; set; }
        public BorrowedBook BorrowedBook { get; set; }
    }
}
