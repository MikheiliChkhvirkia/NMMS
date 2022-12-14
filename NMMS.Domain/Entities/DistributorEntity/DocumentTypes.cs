using NMMS.Common.Domain.Entities;

namespace NMMS.Domain.Entities.DistributorEntity
{
    public class DocumentTypes : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<IdentificationInformation> IdentificationInformations { get; set; }
    }
}
