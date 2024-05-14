using DogRallyManager.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DogRallyManager.Database.Models.Signs;

public class Sign
{
    public int Id { get; set; }
    public string SignType { get; set; } = null!;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
}

public class SignsConfigureration: IEntityTypeConfiguration<Sign>
{
    public void Configure(EntityTypeBuilder<Sign> builder)
    {
        builder.HasData(new List<Sign>()
        {
            new Sign()
            {
                PositionX = 50,
                PositionY = 50,
                Id = 1,
                SignType = "exercise-1.png"
            },
            new Sign()
            {
                PositionX = 100,
                PositionY = 100,
                Id = 2,
                SignType = "exercise-2.png"
            }
        });
    }
}

public interface ISignService
{
    IEnumerable<Sign> GetAllSigns();
    void MoveSigns(int signId, int newXn, int newY);
}

public class SignService(DogRallyDbContext dbContext) : ISignService
{
    public IEnumerable<Sign> GetAllSigns()
    {
        return dbContext.Signs.ToList();
    }

    public void MoveSigns(int signId, int newX, int newY)
    {
        throw new NotImplementedException();
    }
}