using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.User.DTOs
{
    public class EditUserDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public required string Id { get; set; }

        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Fullname must be between 2 and 100 characters.")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 digits.")]
        public required string PhoneNumber { get; set; }
    }
}
