using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.ProductSale;

namespace NMMS.Persistence.Configurations.ProductSalesConfigurations
{
    public class ProductSalesConfigurations : IEntityTypeConfiguration<ProductSales>
    {
        public void Configure(EntityTypeBuilder<ProductSales> builder)
        {
            builder.ToTable("ProductSales");

            builder.HasOne(ps => ps.Distributor)
                .WithMany(d => d.ProductSales)
                .HasForeignKey(ps => ps.ProductsId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(ps => ps.Products)
                .WithMany(d => d.ProductSales)
                .HasForeignKey(ps => ps.ProductsId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
