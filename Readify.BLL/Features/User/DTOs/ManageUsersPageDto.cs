using Readify.BLL.Helpers;

namespace Readify.BLL.Features.User.DTOs
{
    public class ManageUsersPageDto
    {
        public List<UserDto> Users { get; set; } = new List<UserDto>();
        public Metadata Metadata { get; set; } = new Metadata();
    }
}