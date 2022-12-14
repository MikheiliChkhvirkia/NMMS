using NMMS.Common.Domain.Entities;

namespace NMMS.Domain.Entities.DistributorEntity
{
    public class AddressTypes : BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
