namespace Readify.BLL.Features.Book.DTOs
{
    public class RecommendedBooksDto
    {
        public string LastBorrowedBookName { get; set; } = string.Empty;
        public List<BookDto> Books { get; set; } = new List<BookDto>();
        public int IsBorrowing { get; set; }
    }
}
