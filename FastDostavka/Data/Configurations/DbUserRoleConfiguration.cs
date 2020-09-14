
using FastDostavka.Data.Entities.IdentityUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Configurations
{
    public class DbUserRoleConfiguration : IEntityTypeConfiguration<DbUserRole>
    {
        public void Configure(EntityTypeBuilder<DbUserRole> builder)
        {
            builder.HasKey(e => new { e.UserId, e.RoleId });

            builder.HasOne(e => e.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
