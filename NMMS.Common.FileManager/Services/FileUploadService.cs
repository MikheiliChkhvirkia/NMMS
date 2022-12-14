using Microsoft.Extensions.Options;
using NMMS.Common.Exceptions;
using NMMS.Common.FileManager.Interfaces;
using NMMS.Common.FileManager.Models;
using NMMS.Common.FileManager.Tools.Options;

namespace NMMS.Common.FileManager.Services
{
    internal class FileUploadService : IFileUploadService
    {
        private readonly FileManagerOptions options;

        public FileUploadService(IOptions<FileManagerOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<FileResponse> UploadAsync(UploadFileRequest request, CancellationToken cancellationToken)
        {
            if (request.File.Length <= 0)
            {
                throw new AppValidationException("File", "File has no length!");
            }
            string fileExtension = Path.GetExtension(request.File.FileName);

            string fullPath = GetFileFullPath(request.Id.ToString(), fileExtension);

            if (File.Exists(fullPath))
            {
                throw new AppException("File already exists");
            }

            using FileStream stream = new(fullPath, FileMode.CreateNew);
            await request.File.CopyToAsync(stream, cancellationToken);

            return new FileResponse
            {
                Extension = fileExtension,
                FilePath = fullPath,
            };
        }

        #region Private

        private string GetFileFullPath(string fileName, string fileExtension)
        {
            var fullPath = $"{options.StoragePath}\\{fileName}{fileExtension}";
            return fullPath;
        }
        #endregion
    }
}
