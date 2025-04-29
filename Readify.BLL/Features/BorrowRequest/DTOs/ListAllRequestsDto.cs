using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Helpers;

namespace Readify.BLL.Features.BorrowRequest.DTOs
{
    public class ListAllRequestsDto
    {
        public List<BorrowRequestDto> BorrowRequests { get; set; }
        public Metadata Metadata { get; set; } = new Metadata();
    }
}
