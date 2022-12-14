using NMMS.Common.Domain.Entities;

namespace NMMS.Domain.Entities.DistributorEntity
{
    public class Address : TrackedEntity<int>
    {
        public int AddressTypeId { get; set; }
        public string AddressInfo { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual AddressTypes AddressType { get; set; }

    }
}
