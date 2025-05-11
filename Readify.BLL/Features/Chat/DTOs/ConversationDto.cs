namespace Readify.BLL.Features.Chat.DTOs
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MessageCount { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
