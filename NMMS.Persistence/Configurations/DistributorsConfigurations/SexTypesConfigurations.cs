using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Enums.DistributorEnum;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class SexTypesConfigurations : IEntityTypeConfiguration<SexTypes>
    {
        public void Configure(EntityTypeBuilder<SexTypes> builder)
        {
            builder.ToTable("SexTypes");
            builder.HasData(
                new SexTypes
                {
                    Id = (int)SexTypeEnum.Male,
                    Name = "მამრობითი"
                },
                new SexTypes
                {
                    Id = (int)SexTypeEnum.Female,
                    Name = "მდედრობითი"
                }
                );
        }
    }
}
