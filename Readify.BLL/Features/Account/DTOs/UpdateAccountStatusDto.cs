using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readify.BLL.Features.Account.DTOs
{
    public class UpdateAccountStatusDto
    {
        public string UserId { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
