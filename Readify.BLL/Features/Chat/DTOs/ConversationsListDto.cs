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
        public List<ConversationDto> Conversations { get; set; } = new List<ConversationDto>();
        public Metadata Metadata { get; set; } = new Metadata();
    }
}
