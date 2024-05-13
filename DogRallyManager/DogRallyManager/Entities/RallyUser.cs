using Microsoft.AspNetCore.Identity;

namespace DogRallyManager.Entities
{
    public class RallyUser : IdentityUser
    {
        public ICollection<ChatRoom>? AssociatedChatRooms { get; set; } = new List<ChatRoom>();
    }
}
