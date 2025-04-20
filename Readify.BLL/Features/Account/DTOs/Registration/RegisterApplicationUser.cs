using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.Account.DTOs.Registration
{
    public class RegisterApplicationUser
    {
        [Required(ErrorMessage = "Full name is required.")]
        public required string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,}$",
        ErrorMessage = "Password must be at least 6 characters long and include an uppercase letter, a lowercase letter, a number, and a special character (@$!%*?&#).")]
        public required string Password { get; set; }
    }
}