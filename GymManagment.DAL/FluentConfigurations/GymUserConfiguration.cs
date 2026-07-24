using GymManagment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.FluentConfigurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {

            builder.Property(X => X.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(X => X.Email)
               .HasMaxLength(100);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("EmailCheck", "Email Like'_%@_%._%'");  //Basmala@gmail.com
                tb.HasCheckConstraint("PhoneCheck", " Like '010% or Phone Like '011%' or Phone Like '012%'or Phone Like '015%'");

                //Address owned Entity Type
                builder.OwnsOne(X => X.Address, address =>
                {
                    address.Property(X => X.Street).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(30);



                });
               
            });

        }
    }
}
