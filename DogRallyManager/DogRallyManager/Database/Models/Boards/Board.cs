using DogRallyManager.Database.Models.Signs;
using DogRallyManager.Entities;

namespace DogRallyManager.Database.Models.Boards;

public class Board
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Sign> Sign { get; set; } = [];
    public ChatRoom? AssociatedChatRoom { get; set; }
    public ICollection<RallyUser>? ParticipatingUsers{ get; set; } = new List<RallyUser>();
}

