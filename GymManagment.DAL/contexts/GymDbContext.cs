using gym.FluentConfigurations;
using gym.Models;
using GymManagment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace gym.contexts
{
    public class GymDbContext : DbContext

    {
          public GymDbContext(DbContextOptions<GymDbContext> options)  :base(options)    
             {


             }

        public GymDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public DbSet<Plan> Plans { get; set; }
        public DbSet<Member> Members { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<MemberShip> MemberShips { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }


    }
}
