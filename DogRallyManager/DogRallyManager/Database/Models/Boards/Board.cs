using DogRallyManager.Database.Models.Signs;

namespace DogRallyManager.Database.Models.Boards;

public class Board
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Sign> Sign { get; set; } = [];
}

