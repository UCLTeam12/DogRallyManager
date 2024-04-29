using DogRallyManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogRallyManager.DbContexts
{
    public class UserDemoContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDemoContext(DbContextOptions<UserDemoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SEEDING
            modelBuilder.Entity<User>()
                 .HasData(
                new User()
                {
                    UserName = "bl4ck",
                    Id = 1,
                    password = "black",
                    isAdmin = true
                },
                new User()
                {
                    UserName = "Wreck",
                    Id = 2,
                    password = "Wreck",
                    isAdmin = false
                },
                new User()
                {
                    UserName = "admin",
                    Id = 3,
                    password = "admin",
                    isAdmin = false
                }); ;
            base.OnModelCreating(modelBuilder);
        }
    }
}
