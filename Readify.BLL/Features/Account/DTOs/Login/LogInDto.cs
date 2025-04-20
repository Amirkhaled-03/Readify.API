using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.Account.DTOs.Login
{
    public class LogInDto
    {
        [Required(ErrorMessage = "")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "password is required.")]
        public required string Password { get; set; }
    }
}