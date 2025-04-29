namespace Readify.BLL.Features.User.DTOs
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string Fullname { get; set; }
        public required string Username { get; set; }
        public required string PhoneNumber { get; set; }
        public required UserStatus UserStatus { get; set; } = UserStatus.Pending;
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}