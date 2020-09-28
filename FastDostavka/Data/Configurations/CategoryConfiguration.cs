using FastDostavka.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.HasMany(x => x.Stores)
                .WithOne(x => x.Category);
            builder.HasData(
               new Category { Id = 1, Name = "Shop" },
               new Category { Id = 2, Name = "Restaurant" },
               new Category { Id = 3, Name = "Pharmacy" }
               );
        }
    }
}
