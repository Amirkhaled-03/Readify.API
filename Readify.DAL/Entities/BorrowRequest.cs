using Readify.DAL.Entities.Identity;

namespace Readify.DAL.Entities
{
    public class BorrowRequest : BaseEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int BookId { get; set; }

        public string Status { get; set; } // You might want to convert this into an enum
        public DateTime RequestedAt { get; set; }

        public string? ApprovedById { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
        public ApplicationUser? ApprovedBy { get; set; }
    }

}
