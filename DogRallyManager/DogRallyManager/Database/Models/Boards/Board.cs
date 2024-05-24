using DogRallyManager.Database.Models.Signs;
using DogRallyManager.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DogRallyManager.Database.Models.Boards;

public class Board
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Sign> Sign { get; set; } = [];
    public ChatRoom? AssociatedChatRoom { get; set; }
}

