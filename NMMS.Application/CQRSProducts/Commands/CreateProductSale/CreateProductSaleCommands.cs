using AutoMapper;
using MediatR;
using NMMS.Common.Application.Tools.Mappings;
using NMMS.Domain.Entities.ProductSale;

namespace NMMS.Application.CQRSProducts.Commands.CreateProductSale
{
    public class CreateProductSaleCommands : IRequest, IMap<ProductSales>
    {
        public int DistributorId { get; set; }
        public DateTime SellDate { get; set; }
        public int ProductsId { get; set; }
        public double Price { get; set; }
        public double UnitPrice { get; set; }
        public double OverAllPrice { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductSaleCommands, ProductSales>();
        }
    }

}
