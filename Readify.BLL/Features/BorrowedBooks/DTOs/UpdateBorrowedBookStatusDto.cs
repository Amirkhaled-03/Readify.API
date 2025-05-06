using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.BorrowedBooks.DTOs
{
    public class UpdateBorrowedBookStatusDto
    {
        [Required(ErrorMessage = "Request ID is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(BorrowedBookStatus), ErrorMessage = "Invalid value for BorrowedBookStatus.")]
        public BorrowedBookStatus Status { get; set; }

    }
}
