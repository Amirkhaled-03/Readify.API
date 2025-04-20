using Readify.DAL.Entities.Identity;

namespace Readify.DAL.Entities
{
    public class Book : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string ImageUrl { get; set; }
        public int AvailableCount { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ApplicationUser CreatedBy { get; set; }
        public ICollection<BorrowRequest> BorrowRequests { get; set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }

}
