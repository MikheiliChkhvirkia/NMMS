using FluentValidation;

namespace NMMS.Application.CQRSDistributor.Commands.DeleteDistributor
{
    public class DeleteDistributorCommandValidator : AbstractValidator<DeleteDistributorCommand>
    {
        public DeleteDistributorCommandValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);
        }
    }
}
