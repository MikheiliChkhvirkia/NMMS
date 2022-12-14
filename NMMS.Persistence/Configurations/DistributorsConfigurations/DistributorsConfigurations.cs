using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class DistributorsConfigurations : IEntityTypeConfiguration<Distributor>
    {
        public void Configure(EntityTypeBuilder<Distributor> builder)
        {
            builder.ToTable("Distributors");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Distributors FirstName");

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Distributors LastName");

            builder.Property(e => e.BirthDate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("A date and time the file was created.");

            builder.HasOne(d => d.SexTypes)
                .WithMany(i => i.Distributors)
                .HasForeignKey(e => e.SexTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();

            builder.HasOne(d => d.DistributorLevelType)
                .WithMany(i => i.Distributors)
                .HasForeignKey(e => e.SexTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();

            builder.HasOne(d => d.IdentificationInformation)
                .WithOne(d => d.Distributor)
                .HasForeignKey<Distributor>(e => e.IdentificationInformationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Contact)
                .WithOne(d => d.Distributor)
                .HasForeignKey<Distributor>(e => e.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Address)
                .WithOne(d => d.Distributor)
                .HasForeignKey<Distributor>(e => e.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.File)
                .WithOne(d => d.Distributor)
                .HasForeignKey<Distributor>(e => e.FileId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
