using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMMS.Application.Files.Commands.DownloadFile;
using NMMS.Application.Files.Commands.UploadFiles;
using NMMS.Application.Files.Queries.DownloadFile;
using NMMS.Common.API.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace NMMS.API.Controllers.Files
{
    public class FileController : ApiControllerBase
    {
        public FileController(IMediator mediator)
                   : base(mediator) { }

        [HttpPost("upload")]
        [SwaggerOperation("Upload files")]
        public Task<UploadFileModel> Upload(IFormFile file) => mediator.Send(new UploadFilesCommand { File = file });

        [HttpGet("download")]
        [SwaggerOperation("Download file")]
        public IActionResult Download(Guid uniqueId)
        {
            var result = mediator.Send(new DownloadFileQuery { UniqueId = uniqueId });
            return File(result.Result.FileContents,result.Result.ContentType);
        }

        [HttpDelete("delete")]
        [SwaggerOperation("Delete file")]
        public Task Delete(Guid UniqueId) => mediator.Send(new DeleteFileCommand { UniqueId = UniqueId });
    }
}
