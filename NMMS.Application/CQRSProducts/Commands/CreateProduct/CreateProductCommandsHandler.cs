using AutoMapper;
using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Domain.Entities.Product;

namespace NMMS.Application.CQRSProducts.Commands.CreateProduct
{
    public class CreateProductCommandsHandler : IRequestHandler<CreateProductCommands>
    {
        private readonly INmmsDbContext db;
        private readonly IMapper mapper;

        public CreateProductCommandsHandler(INmmsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateProductCommands request, CancellationToken cancellationToken)
        {
            if (ProductExists(request))
                throw new Exception("Product Already Exists");

            var product = mapper.Map<Products>(request.Model);
            product.Code = Guid.NewGuid();

            db.Products.Add(product);

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private bool ProductExists(CreateProductCommands request)
            => db.Products.FirstOrDefault(p => p.Name == request.Model.Name && p.UnitPrice == request.Model.UnitPrice) != null;
    }
}
