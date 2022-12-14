using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Application;
using NMMS.Common.Application.Tools.Extensions;
using NMMS.Domain.Entities.ProductSale;

namespace NMMS.Application.CQRSProducts.Queries.GetProductSales
{
    public class GetProductSalesQueryHandler : IRequestHandler<GetProductSalesQuery, PagedData<ProductSales>>
    {
        private readonly INmmsDbContext db;
        public GetProductSalesQueryHandler(INmmsDbContext db) =>
            this.db = db;

        public Task<PagedData<ProductSales>> Handle(GetProductSalesQuery request, CancellationToken cancellationToken)
        {
            bool filterTextExists = !(string.IsNullOrEmpty(request.Filter) || string.IsNullOrWhiteSpace(request.Filter));
            bool distributorIdExists = request.DistributorId != null;
            bool dateTimeExists = request.Date != null;

            //ToDo: DateTime-ის შესამეწმებლად ასახსნელია ქვედა 2 ხაზს კომენტარი
            //dateTimeExists = true;
            //request.Date = DateTime.Now;

            var result = db.ProductSales.Where(x => (!filterTextExists || x.Products.Name.Contains(request.Filter)) &&
                                                    (!distributorIdExists || x.DistributorId == request.DistributorId) &&
                                                    (!dateTimeExists || x.SellDate > request.Date))
                .Select(p => new ProductSales
                {
                    Id = p.Id,
                    DistributorId = p.DistributorId,
                    SellDate = p.SellDate,
                    ProductsId = p.ProductsId,
                    Price = p.Price,
                    UnitPrice = p.UnitPrice,
                    OverAllPrice = p.OverAllPrice,
                    Distributor = p.Distributor,
                    Products = p.Products
                });

            return Task.FromResult(result.ToPagedData(request.Page, request.Offset));
        }

    }
}
