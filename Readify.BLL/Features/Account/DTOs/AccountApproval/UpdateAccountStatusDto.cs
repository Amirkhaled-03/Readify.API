using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.Account.DTOs.AccountApproval
{
    public class UpdateAccountStatusDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "UserStatus is required.")]
        [EnumDataType(typeof(UserStatus), ErrorMessage = "Invalid value for UserStatus.")]
        public UserStatus UserStatus { get; set; }
    }
}
