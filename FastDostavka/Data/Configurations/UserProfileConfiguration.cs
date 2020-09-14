using FastDostavka.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {

            builder.HasOne(e => e.DbUser)
                .WithOne(e => e.UserProfile)
                .HasForeignKey<UserProfile>(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);         

            builder.Property(e => e.FirstName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.City)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Address)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.RegisterDate)
                .IsRequired();

        }
    }
}
