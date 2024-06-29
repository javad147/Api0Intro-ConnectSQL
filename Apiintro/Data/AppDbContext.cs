using Apiintro.Models;
using Microsoft.EntityFrameworkCore;

namespace Apiintro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option):base(option) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
}
