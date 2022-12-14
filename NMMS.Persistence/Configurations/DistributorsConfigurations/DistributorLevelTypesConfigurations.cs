using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Enums.DistributorEnum;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class DistributorLevelTypesConfigurations : IEntityTypeConfiguration<DistributorLevelTypes>
    {
        public void Configure(EntityTypeBuilder<DistributorLevelTypes> builder)
        {
            builder.ToTable("DistributorLevelTypes");

            builder.HasData(
                new DistributorLevelTypes
                {
                    Id = (int)DistributorLevelTypeEnum.LevelOne,
                    Name = "დონე 1"
                },
                new DistributorLevelTypes
                {
                    Id = (int)DistributorLevelTypeEnum.LevelTwo,
                    Name = "დონე 2"
                },
                new DistributorLevelTypes
                {
                    Id = (int)DistributorLevelTypeEnum.LevelThree,
                    Name = "დონე 3"
                },
                new DistributorLevelTypes
                {
                    Id = (int)DistributorLevelTypeEnum.LevelFour,
                    Name = "დონე 4"
                },
                new DistributorLevelTypes
                {
                    Id = (int)DistributorLevelTypeEnum.LevelFive,
                    Name = "დონე 5"
                }
                );
        }
    }
}
