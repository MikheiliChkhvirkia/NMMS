using Microsoft.Extensions.Options;
using NMMS.Common.Exceptions;
using NMMS.Common.FileManager.Interfaces;
using NMMS.Common.FileManager.Tools.Options;

namespace NMMS.Common.FileManager.Services
{
    internal class FileDeleteService : IFileDeleteService
    {
        private readonly FileManagerOptions options;

        public FileDeleteService(IOptions<FileManagerOptions> options)
        {
            this.options = options.Value;
        }
        public void Delete(string fileName)
        {
            string path = $"{options.StoragePath}\\{fileName}";

            if (!File.Exists(path))
            {
                throw new AppException($"File: {fileName} does not exists");
            }

            File.Delete(path);
        }
    }
}
