namespace Readify.DAL.Entities
{
    public class BookCategory : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
