using Microsoft.Extensions.Options;
using NMMS.Common.Exceptions;
using NMMS.Common.FileManager.Interfaces;
using NMMS.Common.FileManager.Tools.Options;

namespace NMMS.Common.FileManager.Services
{
    internal class FileFetchService : IFileFetchService
    {
        private readonly FileManagerOptions options;

        public FileFetchService(IOptions<FileManagerOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<byte[]> GetBytes(Guid uniqueId, string extention, CancellationToken cancellationToken)
        {
            string path = $"{options.StoragePath}\\{uniqueId}{extention}";

            if (!File.Exists(path))
            {
                throw new ObjectNotFoundException("File", null);
            }

            return await File.ReadAllBytesAsync(path, cancellationToken);
        }
    }
}
