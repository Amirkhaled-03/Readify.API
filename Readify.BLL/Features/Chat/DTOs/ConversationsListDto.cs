using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Helpers;

namespace Readify.BLL.Features.Chat.DTOs
{
    public class ConversationsListDto
    {
        public List<ChatDto> Conversations { get; set; } = new List<ChatDto>();
        public Metadata Metadata { get; set; } = new Metadata();
    }
}
