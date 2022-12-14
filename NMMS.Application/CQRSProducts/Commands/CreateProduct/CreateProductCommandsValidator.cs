using FluentValidation;

namespace NMMS.Application.CQRSProducts.Commands.CreateProduct
{
    public class CreateProductCommandsValidator : AbstractValidator<CreateProductCommands>
    {
        public CreateProductCommandsValidator()
        {
            RuleFor(request => request.Model.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.Model.UnitPrice)
                .NotNull()
                .NotEmpty();
        }
    }
}
