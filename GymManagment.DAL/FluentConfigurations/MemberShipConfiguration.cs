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

    public class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
    {

        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(X => X.CreateAt)
               .HasColumnName("StartDate")
               .HasDefaultValueSql("GETDATE()");

        }
    }
}
