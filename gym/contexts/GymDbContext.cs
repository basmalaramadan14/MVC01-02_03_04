using gym.FluentConfigurations;
using gym.Models;
using Microsoft.EntityFrameworkCore;

namespace gym.contexts
{
    public class GymDbContext : DbContext

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Appseting.JSON
            optionsBuilder.UseSqlServer("Server=.;Database=GymDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Plan>( new planConfiguration());
        }


        public DbSet<Plan> plans { get; set; }
    }
}
