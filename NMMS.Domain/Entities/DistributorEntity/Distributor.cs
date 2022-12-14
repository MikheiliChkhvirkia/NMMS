using NMMS.Common.Domain.Entities;
using NMMS.Domain.Entities.ProductSale;
using File = NMMS.Domain.Entities.Files.File;

namespace NMMS.Domain.Entities.DistributorEntity
{
    public class Distributor : TrackedEntity<int>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int SexTypeId { get; set; }


        public int RecommendedDistributorCount { get; set; }
        public int RecommendedDistributorOverAllCount { get; set; }

        public int RecommendatorDistributorId { get; set; }
        public int DistributorLevelId { get; set; }


        public Guid? FileId { get; set; }
        public int IdentificationInformationId { get; set; }
        public int ContactId { get; set; }
        public int AddressId { get; set; }


        public virtual File File { get; set; }
        public virtual IdentificationInformation IdentificationInformation { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Address Address { get; set; }
        public virtual SexTypes SexTypes { get; set; }
        public virtual DistributorLevelTypes DistributorLevelType { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
    }
}
