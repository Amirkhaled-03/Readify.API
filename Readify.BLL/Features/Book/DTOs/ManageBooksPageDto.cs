using Readify.BLL.Helpers;

namespace Readify.BLL.Features.Book.DTOs
{
    public class ManageBooksPageDto
    {
        public List<BookDto> Books { get; set; } = new List<BookDto>();
        public Metadata Metadata { get; set; } = new Metadata();
    }
}