namespace Readify.BLL.Features.Chat.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public UserType SenderType { get; set; }
        public string UserId { get; set; }
        public string LibrarianId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
    }
}
