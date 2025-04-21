using Microsoft.AspNetCore.Identity;

namespace Readify.DAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public UserStatus UserStatus { get; set; } = UserStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string Fullname { get; set; }

        public required UserType UserType { get; set; }

        // Navigation
        public ICollection<BorrowRequest> BorrowRequests { get; set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}