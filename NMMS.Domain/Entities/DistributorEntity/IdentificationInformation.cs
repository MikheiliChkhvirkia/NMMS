using NMMS.Common.Domain.Entities;

namespace NMMS.Domain.Entities.DistributorEntity
{
    public class IdentificationInformation : TrackedEntity<int>
    {
        public int DocumentTypeId { get; set; }

        public string? DocumentSeries { get; set; }

        public string? DocumentNumber { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime DocumentTerms { get; set; }

        public string IdentityNumber { get; set; }

        public string? IssuingCompany { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual DocumentTypes DocumentTypes { get; set; }

    }
}
