using Readify.BLL.Helpers;

namespace Readify.BLL.Features.Librarian.DTOs
{
    public class ManageLibrarianPageDto
    {
        public List<LibrarianDto> Librarians { get; set; } = new List<LibrarianDto>();
        public Metadata Metadata { get; set; } = new Metadata();
    }
}