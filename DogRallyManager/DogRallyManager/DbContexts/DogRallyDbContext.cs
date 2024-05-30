using DogRallyManager.Database.Models.Boards;
using DogRallyManager.Database.Models.Signs;
using DogRallyManager.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogRallyManager.DbContexts
{
    public class DogRallyDbContext : IdentityDbContext
    {
        public DbSet<RallyUser> RallyUsers { get; set; } 
        public DbSet<RallyUserRole> RallyUserRoles { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Sign> Signs { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DogRallyDbContext(DbContextOptions<DogRallyDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new SignsConfigureration());
            builder.Entity<RallyUserRole>(x => x.HasData(new List<RallyUserRole>()
            {
                new RallyUserRole()
                {
                    Name = "Admin",
                    Id = "495ad32f-2d1d-49b9-976c-f6e6d1755418",
                    NormalizedName = "ADMIN",
                }

            }));
        }
    }
}
