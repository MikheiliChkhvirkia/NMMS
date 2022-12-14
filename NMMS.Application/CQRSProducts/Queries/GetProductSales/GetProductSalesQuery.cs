using MediatR;
using NMMS.Common.Application;
using NMMS.Domain.Entities.ProductSale;

namespace NMMS.Application.CQRSProducts.Queries.GetProductSales
{
    public class GetProductSalesQuery: IRequest<PagedData<ProductSales>> 
    {
        public DateTime? Date { get; set; }
        public string? Filter { get; set; }
        public int? DistributorId { get; set; }
        public int Page { get; set; } = 1;
        public int Offset { get; set; } = 10;
    }
}
