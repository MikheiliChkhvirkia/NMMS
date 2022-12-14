using FluentValidation;

namespace NMMS.Application.CQRSDistributor.Commands.UpdateDistributor
{
    public class UpdateDistributorCommandValidator : AbstractValidator<UpdateDistributorCommand>
    {
        public UpdateDistributorCommandValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);
            RuleFor(request => request.Model.FirstName)
                .NotNull()
                .NotEmpty()
                .Length(1, 50);
            RuleFor(request => request.Model.LastName)
                .NotNull()
                .NotEmpty()
                .Length(1, 50);
            RuleFor(request => request.Model.BirthDate)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.Model.SexTypeId)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(2);
        }
    }
}
