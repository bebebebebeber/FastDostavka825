using FastDostavka.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.Adress)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.Image)
                .HasMaxLength(128);


            builder.HasOne(x => x.Category)
                .WithMany(x => x.Stores);

        }
    }
}
