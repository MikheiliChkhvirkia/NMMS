using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NMMS.Application.Files.Queries.DownloadFile
{
    public class DownloadFileQuery : IRequest<FileContentResult>
    {
        public Guid UniqueId { get; set; }
    }
}
