namespace DogRallyManager.Database.Models.Signs;

public class Sign
{
    public int Id { get; set; }
    public string SignType { get; set; } = null!;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
}

public interface ISignService
{
    IEnumerable<Sign> GetAllSigns();
    void MoveSigns(int signId, int newXn, int newY);
}