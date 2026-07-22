using gym.FluentConfigurations;
using gym.Models;
using Microsoft.EntityFrameworkCore;

namespace gym.contexts
{
    public class GymDbContext : DbContext

    {
          public GymDbContext(DbContextOptions<GymDbContext> options)  :base(options)    
             {


             }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Plan>( new planConfiguration());
        }


        public DbSet<Plan> plans { get; set; }
    }
}
