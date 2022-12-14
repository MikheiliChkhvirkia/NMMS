using NMMS.Common.Domain.Entities;

namespace NMMS.Domain.Entities.DistributorEntity
{
    public class DistributorLevelTypes : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Distributor> Distributors { get; set; }
    }
}
