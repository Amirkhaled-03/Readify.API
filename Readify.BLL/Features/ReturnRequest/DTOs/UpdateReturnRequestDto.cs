using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Features.ReturnRequest.DTOs
{
    public class UpdateReturnRequestDto
    {
        public int Id { get; set; }
        public ReturnRequestStatus Status { get; set; }
    }
}
