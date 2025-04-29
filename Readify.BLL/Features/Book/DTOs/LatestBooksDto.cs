using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Features.Book.DTOs
{
    public class LatestBooksDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public int AvailableCount { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
