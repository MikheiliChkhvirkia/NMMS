using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Enums.DistributorEnum;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class AddressTypesConfigurations : IEntityTypeConfiguration<AddressTypes>
    {
        public void Configure(EntityTypeBuilder<AddressTypes> builder)
        {
            builder.ToTable("AddressType");

            builder.HasData(
                new AddressTypes
                {
                    Id = (int)AddressTypeEnum.Actual,
                    Name = "ფაქტიური მისამართი"
                },
                new AddressTypes
                {
                    Id = (int)AddressTypeEnum.Registration,
                    Name = "რეგისტრაციის მისამართი"
                }
                );
        }
    }
}
