using NMMS.Common.Domain.Entities;

namespace NMMS.Domain.Entities.DistributorEntity
{
    public class ContactTypes : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
