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
    internal class TrainerConfiguration : GymUserConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {

            builder.Property(X => X.CreateAt)
                    .HasColumnName("HireDate")
                    .HasDefaultValueSql("GETDATE()");

            //Important: call the base class's Configure 
            base.Configure(builder);

        }
    }
}
