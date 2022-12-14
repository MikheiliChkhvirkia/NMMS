using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class ContactsConfigurations : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.Property(e => e.ContactInformation)
                .HasMaxLength(100);
        }
    }
}
