namespace Readify.DAL.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
