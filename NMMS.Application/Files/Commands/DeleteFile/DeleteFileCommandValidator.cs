using FluentValidation;

namespace NMMS.Application.Files.Commands.DownloadFile
{
    public class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileCommandValidator()
        {
            RuleFor(request => request.UniqueId)
                .NotNull()
                .NotEmpty()
                .NotEqual(default(Guid));
        }
    }
}
