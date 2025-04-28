namespace Readify.DAL.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
