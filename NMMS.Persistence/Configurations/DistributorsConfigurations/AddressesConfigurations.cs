using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class AddressesConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.Property(e => e.AddressInfo)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(ad => ad.AddressType)
                .WithMany(adt => adt.Addresses)
                .HasForeignKey(ad => ad.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
