using FluentValidation;

namespace NMMS.Application.CQRSIdentification.Commands.UpdateIdentification
{
    public class UpdateIdentificationCommandValidator : AbstractValidator<UpdateIdentificationCommand>
    {
        public UpdateIdentificationCommandValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);
            RuleFor(request => request.Model.DocumentSeries)
                .Length(1,10);
            RuleFor(request => request.Model.DocumentNumber)
                .Length(1, 10);
            RuleFor(request => request.Model.IdentityNumber)
                .Length(1, 50);
            RuleFor(request => request.Model.IssuingCompany)
                .Length(1, 10);
        }
    }
}
