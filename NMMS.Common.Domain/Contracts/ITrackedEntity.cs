namespace NMMS.Common.Persistence.Contracts
{
    public interface ITrackedEntity
    {
        DateTime? DeleteDate { get; protected set; }
        void UpdateCreateCredentials(DateTime createDate);
        void UpdateDeleteCredentials(DateTime deleteDate);
    }
}
