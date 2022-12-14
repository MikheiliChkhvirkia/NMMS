using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Enums.DistributorEnum;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class DocumentTypesConfigurations : IEntityTypeConfiguration<DocumentTypes>
    {
        public void Configure(EntityTypeBuilder<DocumentTypes> builder)
        {
            builder.ToTable("DocumentTypes");

            builder.HasData(
                new DocumentTypes
                {
                    Id = (int)DocumentTypeEnum.IDCard,
                    Name = "პირადობის მოწმობა"
                },
                new DocumentTypes
                {
                    Id = (int)DocumentTypeEnum.Passport,
                    Name = "პასპორტი"
                }
                );

            builder.HasMany(a => a.IdentificationInformations)
                .WithOne(a => a.DocumentTypes)
                .HasForeignKey(a => a.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

    }
}
