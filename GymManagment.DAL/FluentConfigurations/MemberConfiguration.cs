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
    internal class MemberConfiguration : GymUserConfiguration<Member>,IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {

            builder.Property(X => X.CreateAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");

            //Important: call the base class's Configure 
            base.Configure(builder);

        }

    }
}
