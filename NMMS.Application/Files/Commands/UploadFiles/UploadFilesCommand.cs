using MediatR;
using Microsoft.AspNetCore.Http;

namespace NMMS.Application.Files.Commands.UploadFiles
{
    public class UploadFilesCommand : IRequest<UploadFileModel>
    {
        public IFormFile File { get; set; }
    }
}