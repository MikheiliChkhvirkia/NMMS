using NMMS.Common.FileManager.Models;

namespace NMMS.Common.FileManager.Interfaces
{
    public interface IFileUploadService
    {
        Task<FileResponse> UploadAsync(UploadFileRequest file, CancellationToken cancellationToken);
    }
}
