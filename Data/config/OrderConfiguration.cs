using Back_End.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Back_End.Data.config
{
    public partial class ProductConfiguration
    {
        public class OrderConfiguration : IEntityTypeConfiguration<Order>
        {
            public void Configure(EntityTypeBuilder<Order> builder)
            {
                builder.HasKey(x => x.Id);

                builder.ToTable("Orders");

                builder.HasOne(o => o.Product)
                    .WithMany(o => o.Orders)
                    .HasForeignKey(o => o.ProductId);
            }
        }
    }
}
