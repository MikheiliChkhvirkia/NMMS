using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.FileManager.Interfaces;
using NMMS.Common.Tools.Extensions;


namespace NMMS.Application.Files.Commands.DownloadFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
    {
        private readonly INmmsDbContext db;
        private readonly IFileDeleteService fileDeleteService;

        public DeleteFileCommandHandler(
            INmmsDbContext db,
            IFileDeleteService fileDeleteService)
        {
            this.db = db;
            this.fileDeleteService = fileDeleteService;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = db.Files.FirstOrDefault(file => file.Id == request.UniqueId);
            file.EnsureNotNull();

            fileDeleteService.Delete($"{file.Id}{file.Extension}");

            file.DateDeleted = DateTime.Now;

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
