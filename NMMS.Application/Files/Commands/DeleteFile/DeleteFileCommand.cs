using MediatR;

namespace NMMS.Application.Files.Commands.DownloadFile
{
    public class DeleteFileCommand : IRequest
    {
        public Guid UniqueId { get; set; }
    }
}
