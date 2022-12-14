using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.FileManager.Interfaces;
using NMMS.Common.Tools.Extensions;

namespace NMMS.Application.Files.Queries.DownloadFile
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, FileContentResult>
    {
        private readonly IFileFetchService fileFetchService;
        private readonly INmmsDbContext db;

        public DownloadFileQueryHandler(IFileFetchService fileFetchService, INmmsDbContext db)
        {
            this.fileFetchService = fileFetchService;
            this.db = db;
        }

        public async Task<FileContentResult> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var fileInfo = db.Files.FirstOrDefault(file => file.Id == request.UniqueId);

            fileInfo.EnsureNotNull();

            var fileBytes = await fileFetchService.GetBytes(request.UniqueId, fileInfo.Extension, cancellationToken);

            return new FileContentResult(fileBytes, fileInfo.MimeType)
            {
                FileDownloadName = fileInfo.Name
            };
        }
    }
}
