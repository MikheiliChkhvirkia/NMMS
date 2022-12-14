using FluentValidation;

namespace NMMS.Application.Files.Commands.UploadFiles
{
    public class UploadFilesCommandValidator : AbstractValidator<UploadFilesCommand>
    {
        public UploadFilesCommandValidator()
        {
            RuleFor(request => request.File)
                .NotNull()
                .NotEmpty();
        }
    }
}
