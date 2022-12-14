using NMMS.Common.Persistence.Contracts;

namespace NMMS.Common.Domain.Entities
{
    public abstract class TrackedEntity<T> : BaseEntity<T>, ITrackedEntity
    {
        public DateTime CreateDate { get; protected set; }
        public DateTime? DeleteDate { get; set; }

        public void Delete()
        {
            DeleteDate = DateTime.Now;
        }

        public void UpdateCreateCredentials(DateTime createDate)
        {
            CreateDate = createDate;
        }

        public void UpdateDeleteCredentials(DateTime deleteDate)
        {
            DeleteDate = deleteDate;
        }
    }
}
