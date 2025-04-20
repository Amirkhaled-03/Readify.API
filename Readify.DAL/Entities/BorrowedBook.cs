using Readify.DAL.Entities.Identity;

namespace Readify.DAL.Entities
{
    public class BorrowedBook : BaseEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int BookId { get; set; }

        public DateTime BorrowedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; } // if it has value, this means that the book is returned

        public string? ConfirmedById { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
        public ApplicationUser? ConfirmedBy { get; set; }
    }

}
