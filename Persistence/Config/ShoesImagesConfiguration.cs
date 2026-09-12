using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Config
{
    public class ShoesImagesConfiguration : IEntityTypeConfiguration<ShoesImages>
    {
        public void Configure(EntityTypeBuilder<ShoesImages> builder)
        {
            builder.HasKey(si => si.Id);

            builder.Property(si => si.ImageUrl)
                .IsRequired();

            builder.HasOne(si => si.Shoes)
                .WithMany(s => s.Images)
                .HasForeignKey(si => si.ShoesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
