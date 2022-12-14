using AutoMapper;
using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Domain.Entities.ProductSale;

namespace NMMS.Application.CQRSProducts.Commands.CreateProductSale
{
    public class CreateProductSaleCommandsHandler : IRequestHandler<CreateProductSaleCommands>
    {
        private readonly INmmsDbContext db;
        private readonly IMapper mapper;

        public CreateProductSaleCommandsHandler(INmmsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateProductSaleCommands request, CancellationToken cancellationToken)
        {
            if (DistributorExists(request.DistributorId))
                throw new Exception("Distributor does not exist");
            if (ProductExists(request.ProductsId))
                throw new Exception("Product does not exist");

            var productSale = mapper.Map<ProductSales>(request);
            
            db.ProductSales.Add(productSale);

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        private bool DistributorExists(int id) 
            => db.Distributors.FirstOrDefault( d => d.Id == id) == null;
        private bool ProductExists(int id)
            => db.Products.FirstOrDefault(d => d.Id == id) == null;
    }
}
