using DogRallyManager.Database.Models.Signs;
using DogRallyManager.DbContexts;

namespace DogRallyManager.Repositories;

public class SignRepository(DogRallyDbContext context) : ISignService
{
    public IEnumerable<Sign> GetAllSigns()
    {
        return context.Signs.ToList();
    }

    public void MoveSigns(int signId, int newX, int newY)
    {
        var sign = context.Signs.Find(signId);
        if (sign == null) return;
        sign.PositionX = newX;
        sign.PositionY = newY;
        context.SaveChanges();
    }
}