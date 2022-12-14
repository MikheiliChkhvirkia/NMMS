using FluentValidation;

namespace NMMS.Application.CQRSProducts.Commands.CreateProductSale
{
    public class CreateProductSaleCommandsValidator : AbstractValidator<CreateProductSaleCommands>
    {
        public CreateProductSaleCommandsValidator()
        {
            RuleFor(request => request.DistributorId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(request => request.SellDate)
                .NotEmpty()
                .NotNull();
            RuleFor(request => request.ProductsId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(request => request.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(request => request.UnitPrice)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(request => request.OverAllPrice)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }
    }
}
