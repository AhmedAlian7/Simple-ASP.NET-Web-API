using Back_End.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Back_End.Data.config
{
    public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
               .HasMaxLength(55).IsRequired();


            builder.Property(p => p.Price)
                .HasPrecision(15, 2);

            builder.ToTable("Products");
        }
    }
}
