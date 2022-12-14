using FluentValidation;

namespace NMMS.Application.Files.Queries.DownloadFile
{
    public class DownloadFileQueryValidator : AbstractValidator<DownloadFileQuery>
    {
        public DownloadFileQueryValidator()
        {
            RuleFor(request => request.UniqueId)
                .NotNull()
                .NotEmpty()
                .NotEqual(default(Guid));
        }
    }
}
