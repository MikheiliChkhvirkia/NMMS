using AutoMapper;
using MediatR;
using NMMS.Common.Application.Tools.Mappings;
using NMMS.Domain.Entities.Product;

namespace NMMS.Application.CQRSProducts.Commands.CreateProduct
{
    public class CreateProductCommands : IRequest, IMap<Products>
    {
        public ProductsModel Model { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductsModel, Products>();
        }
    }
    public class ProductsModel
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}
