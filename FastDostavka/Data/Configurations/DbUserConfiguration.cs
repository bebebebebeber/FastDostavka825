
using FastDostavka.Data.Entities.IdentityUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Configurations
{
    public class DbUserConfiguration : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User);
            builder.HasOne(e => e.UserProfile)
                    .WithOne(e => e.DbUser);
        }
    }
}
