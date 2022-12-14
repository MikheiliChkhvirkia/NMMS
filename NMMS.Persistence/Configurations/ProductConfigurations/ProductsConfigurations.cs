using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.Product;

namespace NMMS.Persistence.Configurations.ProductConfigurations
{
    public class ProductsConfigurations : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");

            builder.HasData(
                new Products
                {
                    Id = 1,
                    Name = "კალამი",
                    Code = Guid.NewGuid(),
                    UnitPrice = .1d
                },
                new Products
                {
                    Id = 2,
                    Name = "ფანქარი",
                    Code = Guid.NewGuid(),
                    UnitPrice = .1d
                },
                new Products
                {
                    Id = 3,
                    Name = "ქაღალდი",
                    Code = Guid.NewGuid(),
                    UnitPrice = 10d
                },
                new Products
                {
                    Id = 4,
                    Name = "მაკრატელი",
                    Code = Guid.NewGuid(),
                    UnitPrice = 5d
                },
                new Products
                {
                    Id = 5,
                    Name = "ფლომასტერი",
                    Code = Guid.NewGuid(),
                    UnitPrice = 2.5d
                }
                );
        }
    }
}
