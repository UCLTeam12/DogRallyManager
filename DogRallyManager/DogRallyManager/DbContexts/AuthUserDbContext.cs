using DogRallyManager.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogRallyManager.DbContexts
{
    public class AuthUserDbContext : IdentityDbContext
    {
        public DbSet<RallyUser> RallyUsers { get; set; } 

        public DbSet<RallyUserRole> RallyUserRoles { get; set; }

        public AuthUserDbContext(DbContextOptions<AuthUserDbContext> options) : base(options)
        {
            
        }

    }
}
