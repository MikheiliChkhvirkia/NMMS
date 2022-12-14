using FluentValidation;

namespace NMMS.Application.CQRSContact.Commands.UpdateContact
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);
            RuleFor(request => request.Model.ContactTypeId)
                .GreaterThan(0).LessThanOrEqualTo(4);
        }
    }
}
