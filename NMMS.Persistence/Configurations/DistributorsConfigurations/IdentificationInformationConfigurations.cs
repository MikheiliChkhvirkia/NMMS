using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Persistence.Configurations.DistributorsConfigurations
{
    public class IdentificationInformationConfigurations : IEntityTypeConfiguration<IdentificationInformation>
    {
        public void Configure(EntityTypeBuilder<IdentificationInformation> builder)
        {
            builder.ToTable("IdentificationInformations");

            builder.Property(e => e.DocumentSeries)
                .HasMaxLength(10);

            builder.Property(e => e.DocumentNumber)
                .HasMaxLength(10);

            builder.Property(e => e.ReleaseDate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("A date and time the file was created.");

            builder.Property(e => e.DocumentTerms)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("A date and time the file was created.");

            builder.Property(e => e.IdentityNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.IssuingCompany)
                .HasMaxLength(10);
        }
    }
}
