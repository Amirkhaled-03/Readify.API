using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Features.BorrowRequest.DTOs
{
    public class CreateBorrowRequestDto
    {
        [Required(ErrorMessage = "Must enter book ID")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Must choose start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Must choose end date")]
        public DateTime EndDate { get; set; }
    }
}
