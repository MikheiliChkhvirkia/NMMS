using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Enums.DistributorEnum;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class ContactTypesConfigurations : IEntityTypeConfiguration<ContactTypes>
    {
        public void Configure(EntityTypeBuilder<ContactTypes> builder)
        {
            builder.ToTable("ContactTypes");

            builder.HasData(
                new ContactTypes
                {
                    Id = (int)ContactTypeEnum.Phone,
                    Name = "ტელეფონი"
                },
                new ContactTypes
                {
                    Id = (int)ContactTypeEnum.Mobile,
                    Name = "მობილური"
                },
                new ContactTypes
                {
                    Id = (int)ContactTypeEnum.Email,
                    Name = "ელ.ფოსტა"
                },
                new ContactTypes
                {
                    Id = (int)ContactTypeEnum.Fax,
                    Name = "ფაქსი"
                }
                );

            builder.HasMany(a => a.Contacts)
                .WithOne(a => a.ContactType)
                .HasForeignKey(a => a.ContactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
