using Readify.DAL.Entities.Identity;

namespace Readify.DAL.Entities
{
    public class Message : BaseEntity
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public UserType SenderType { get; set; }
        public string UserId { get; set; }
        public string LibrarianId { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }

        public Conversation Conversation { get; set; }
        public ApplicationUser User {  get; set; }
        public ApplicationUser Librarian { get; set; }

    }
}
