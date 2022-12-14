using NMMS.Common.Domain.Entities;

namespace NMMS.Domain.Entities.DistributorEntity
{
    public class Contact : TrackedEntity<int>
    {
        public int ContactTypeId { get; set; }
        public string ContactInformation { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual ContactTypes ContactType { get; set; }
    }
}
