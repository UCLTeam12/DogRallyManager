using DogRallyManager.Database.Models.Boards;
using DogRallyManager.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DogRallyManager.Database.Models.Signs;

public class Sign
{
    public int Id { get; set; }
    public string SignType { get; set; } = null!;
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }

    public bool IsPlaced()
    {
        return PositionX != null || PositionY != null;
    }
    public Board Board { get; set; }
}

public class SignsConfigureration: IEntityTypeConfiguration<Sign>
{
    public void Configure(EntityTypeBuilder<Sign> builder)
    {
       
    }
}
