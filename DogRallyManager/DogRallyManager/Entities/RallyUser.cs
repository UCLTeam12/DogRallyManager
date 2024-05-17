using Microsoft.AspNetCore.Identity;

namespace DogRallyManager.Entities
{
    public class RallyUser : IdentityUser
    {
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<ChatRoom>? AssociatedChatRooms { get; set; } = new List<ChatRoom>();
    }
}
