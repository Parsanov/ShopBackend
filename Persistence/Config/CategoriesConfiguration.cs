using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Config
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.NameCategory).IsRequired().HasMaxLength(100);
            builder.HasMany(c => c.Shoes)
                   .WithOne(s => s.Categories)
                   .HasForeignKey(s => s.CategoriesId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
