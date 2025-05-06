namespace Readify.BLL.Features.Book.DTOs
{
    public class BookDetailsDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required string Description { get; set; }
        public required int AvailableCount { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        // as base64
        public string? Image { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }
}