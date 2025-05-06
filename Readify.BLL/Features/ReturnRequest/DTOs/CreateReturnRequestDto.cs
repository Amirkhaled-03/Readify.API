using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.ReturnRequest.DTOs
{
    public class CreateReturnRequestDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int BorrowedBookId { get; set; }
        [Required(ErrorMessage = "Return Date is required.")]
        public DateTime ReturnDate { get; set; }
    }
}
