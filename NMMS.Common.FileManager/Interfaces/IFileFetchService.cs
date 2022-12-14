namespace NMMS.Common.FileManager.Interfaces
{
    public interface IFileFetchService
    {
        Task<byte[]> GetBytes(Guid uniqueId, string extention, CancellationToken cancellationToken);
    }
}
