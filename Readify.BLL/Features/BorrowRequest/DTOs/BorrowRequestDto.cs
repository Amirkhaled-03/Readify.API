using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.Book.DTOs;

namespace Readify.BLL.Features.BorrowRequest.DTOs
{
    public class BorrowRequestDto
    {
        public int Id { get; set; }
        public string RequestedBy { get; set; }
        public string PhoneNumber { get; set; }
        public string? ApprovedBy { get; set; }
        public string BookTitle { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BorrowRequestStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
