using Readify.DAL.Entities.Identity;

namespace Readify.DAL.Entities
{
    public class BorrowRequest : BaseEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int BookId { get; set; }

        public BorrowRequestStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? ApprovedById { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
        public ApplicationUser? ApprovedBy { get; set; }
    }
}