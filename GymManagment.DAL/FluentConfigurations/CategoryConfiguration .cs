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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(X => X.CategoryName)
                .HasColumnType("varchar")
                .HasMaxLength(30);

            builder.Property(X => X.CreateAt)
                   .HasDefaultValueSql("GETDATE()");

            //seeding cardio, Strength,Yoga ,Boxing , CrossFit
            //HasData => Must Send 'ID'
            builder.HasData(
              new Category { Id = 5, CategoryName = "Cardio" },
              new Category { Id =1 , CategoryName = "Strength" },
              new Category { Id = 2, CategoryName = "Yoga" },
              new Category { Id = 3, CategoryName = "Boxing" },
              new Category { Id = 4, CategoryName = "CrossFit" }
                );
        }
    }
}
