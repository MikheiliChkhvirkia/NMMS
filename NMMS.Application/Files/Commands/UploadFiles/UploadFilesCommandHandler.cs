using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Exceptions;
using NMMS.Common.FileManager.Interfaces;
using NMMS.Common.FileManager.Models;
using File = NMMS.Domain.Entities.Files.File;

namespace NMMS.Application.Files.Commands.UploadFiles
{
    public class UploadFilesCommandHandler : IRequestHandler<UploadFilesCommand, UploadFileModel>
    {
        private readonly INmmsDbContext db;
        private readonly IFileUploadService fileUploadService;

        public UploadFilesCommandHandler(INmmsDbContext db,
            IFileUploadService fileUploadService)
        {
            this.db = db;
            this.fileUploadService = fileUploadService;
        }

        public async Task<UploadFileModel> Handle(UploadFilesCommand request, CancellationToken cancellationToken)
        {
            string fileName = Path.GetFileNameWithoutExtension(request.File.FileName);

            Guid uniqueId = Guid.NewGuid();

            FileResponse fileSavedToStorage = await fileUploadService.UploadAsync(new UploadFileRequest
            {
                File = request.File,
                Id = uniqueId
            }, cancellationToken);

            if (fileSavedToStorage == null)
            {
                throw new AppException("File is not saved in storage");
            }

            int? fileNameSufixNumber = GenerateSufixNumber(fileName);

            await db.Files.AddAsync(new File
            {
                Id = uniqueId,
                Name = fileName,
                FileNameSufixNumber = fileNameSufixNumber,
                Extension = fileSavedToStorage.Extension,
                PhysicalPath = fileSavedToStorage.FilePath,
                MimeType = request.File.ContentType,
                LengthInBytes = request.File.Length
            }, cancellationToken);


            await db.SaveChangesAsync(cancellationToken);

            return new UploadFileModel
            {
                FileName = fileNameSufixNumber == null ? fileName : $"{fileName} ({fileNameSufixNumber})",
                Extension = fileSavedToStorage.Extension,
                Id = uniqueId
            };
        }

        #region Private
        public int? GenerateSufixNumber(string fileName)
        {
            IQueryable<File> filesWithSameFileName = db.Files.Where(file => fileName == file.Name && file.DateDeleted == null);

            int? sufixNumber = null;

            if (filesWithSameFileName != null && filesWithSameFileName.Any())
            {
                var sufix = filesWithSameFileName.Max(file => file.FileNameSufixNumber);
                sufixNumber = sufix + 1;
            }
            return sufixNumber;
        }
        #endregion
    }
}
