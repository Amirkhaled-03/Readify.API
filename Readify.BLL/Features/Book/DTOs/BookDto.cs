namespace Readify.BLL.Features.Book.DTOs
{
    public class BookDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public int AvailableCount { get; set; }
        public DateTime CreatedAt { get; set; }

        // as base64
        public string? Image { get; set; }
    }
}