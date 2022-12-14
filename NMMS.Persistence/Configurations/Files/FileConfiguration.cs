using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = NMMS.Domain.Entities.Files.File;

namespace NMMS.Persistence.Configurations.Files
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("File");

            builder.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("A date and time the file was created.");

            builder.Property(e => e.DateDeleted)
                .HasColumnType("datetime")
                .HasComment("A date and time the file was deleted.");

            builder.Property(e => e.Extension)
                .IsRequired()
                .HasMaxLength(5)
                .HasComment("An extension of the file.");

            builder.Property(e => e.LengthInBytes).HasComment("A size of the file in bytes.");

            builder.Property(e => e.MimeType)
                .IsRequired()
                .HasComment("A MIME type of the file.");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(260)
                .HasComment("A name of the file.");
            builder.Property(e => e.FileNameSufixNumber)
                .HasMaxLength(50)
                .HasComment("A sufix of the file.");

            builder.Property(e => e.PhysicalPath)
                .IsRequired()
                .HasComment("A physical path of the file.");

            Guid newGuid = Guid.NewGuid();
            builder.HasData(
                new File
                {
                    Id = newGuid,
                    Name = "DefaultFile",
                    Extension = "jpg",
                    PhysicalPath = $"D:\\NMMSFiles\\{newGuid}",
                    MimeType = "image/jpeg",
                    LengthInBytes = 10803564,
                    DateCreated = DateTime.Now
                }
                );

        }
    }
}
