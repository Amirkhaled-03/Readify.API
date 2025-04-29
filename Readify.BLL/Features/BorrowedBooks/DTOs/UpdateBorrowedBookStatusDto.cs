using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Features.BorrowedBooks.DTOs
{
    public class UpdateBorrowedBookStatusDto
    {
        [Required(ErrorMessage = "Request ID is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public BorrowedBookStatus Status { get; set; }

    }
}
