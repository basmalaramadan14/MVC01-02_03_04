using gym.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gym.FluentConfigurations
{
    public class planConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(30);
            

            builder.Property(p => p.Description)

                  .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasPrecision(10,2);

            builder.Property(p => p.CreateAt)
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable(TB =>

            {
                TB.HasCheckConstraint("PlanDurationCheck", "DurationDays Between 1 and 365");
            });



        }
    }
}
