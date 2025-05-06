using System.ComponentModel.DataAnnotations;

namespace Readify.BLL.Features.ReturnRequest.DTOs
{
    public class UpdateReturnRequestDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(ReturnRequestStatus), ErrorMessage = "Invalid value for ReturnRequestStatus.")]
        public ReturnRequestStatus Status { get; set; }
    }
}
