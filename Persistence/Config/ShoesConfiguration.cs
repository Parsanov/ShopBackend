using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ShoesConfiguration : IEntityTypeConfiguration<Shoes>
    {
        public void Configure(EntityTypeBuilder<Shoes> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.BrandName).IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Price).IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(s => s.VendorCode).IsRequired()
                .HasMaxLength(50);

            builder.HasMany(s => s.ShoesSizes)
                .WithOne(ss => ss.Shoes)
                .HasForeignKey(ss => ss.ShoesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Images)
                .WithOne(si => si.Shoes)
                .HasForeignKey(si => si.ShoesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Categories)
                .WithMany(c => c.Shoes)
                .HasForeignKey(s => s.CategoriesId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
