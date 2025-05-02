using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Helpers;

namespace Readify.BLL.Features.ReturnRequest.DTOs
{
    public class ListReturnRequestsDto
    {
        public List<ReturnRequestDto> ReturnRequests { get; set; }
        public Metadata Metadata { get; set; } = new Metadata();

    }
}
