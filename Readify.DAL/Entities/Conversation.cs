using Readify.DAL.Entities.Identity;

namespace Readify.DAL.Entities
{
    public class Conversation : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ApplicationUser User { get; set; }
    }
}
