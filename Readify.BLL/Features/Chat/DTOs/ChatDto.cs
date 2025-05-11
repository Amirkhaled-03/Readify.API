namespace Readify.BLL.Features.Chat.DTOs
{
    public class ChatDto
    {
        public int ConversationId { get; set; }
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
        public int MessagesCount => Messages?.Count ?? 0;
        public List<MessageDto> Messages { get; set; }
    }
}
