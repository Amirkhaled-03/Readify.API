using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.Book.DTOs;
using Readify.BLL.Helpers;

namespace Readify.BLL.Features.BorrowedBooks.DTOs
{
    public class ManageBorrowedBooksDto
    {
        public List<BorrowedBookDto> BorrowedBooks { get; set; } = new List<BorrowedBookDto>();
        public Metadata Metadata { get; set; } = new Metadata();
    }
}
