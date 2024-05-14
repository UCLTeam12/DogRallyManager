using DogRallyManager.Database.Models.Signs;
using DogRallyManager.Entities;
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

        public DogRallyDbContext(DbContextOptions<DogRallyDbContext> options) : base(options)
        {
            
        }

    }
}
