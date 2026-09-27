using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ShoesSizesConfiguration : IEntityTypeConfiguration<ShoesSize>
    {
        public void Configure(EntityTypeBuilder<ShoesSize> builder)
        {
            builder.HasKey(ss => ss.Id);

            builder.Property(ss => ss.Size)
                .IsRequired();

            builder.Property(ss => ss.QuantityInStock)
                .IsRequired();

            builder.HasOne(ss => ss.Shoes)
                .WithMany(s => s.ShoesSizes)
                .HasForeignKey(ss => ss.ShoesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
