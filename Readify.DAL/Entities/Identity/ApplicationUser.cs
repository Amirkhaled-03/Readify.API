using Microsoft.AspNetCore.Identity;

namespace Readify.DAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<BorrowRequest> BorrowRequests { get; set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }

}
