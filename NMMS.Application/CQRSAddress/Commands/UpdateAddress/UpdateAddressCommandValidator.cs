using FluentValidation;

namespace NMMS.Application.CQRSAddress.Commands.UpdateAddress
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);
            RuleFor(request => request.Model.AddressTypeId)
                .GreaterThan(0).LessThanOrEqualTo(2);
        }
    }
}
