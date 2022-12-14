using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMMS.Application.CQRSProducts.Commands.CreateProduct;
using NMMS.Application.CQRSProducts.Commands.CreateProductSale;
using NMMS.Application.CQRSProducts.Queries.GetProducts;
using NMMS.Application.CQRSProducts.Queries.GetProductSales;
using NMMS.Common.API.Controllers;
using NMMS.Common.Application;
using NMMS.Common.Dto;
using NMMS.Domain.Entities.ProductSale;
using Swashbuckle.AspNetCore.Annotations;

namespace NMMS.API.Controllers.Product
{
    public class ProductsController : ApiControllerBase
    {
        public ProductsController(IMediator mediator)
            : base(mediator) { }

        [HttpGet("GetProducts")]
        [SwaggerOperation("Get products")]
        public Task<List<NamedData<int>>> GetProducts() => mediator.Send(new GetProductsQuery());

        [HttpGet("GetProductSales")]
        [SwaggerOperation("Get product sales")]
        public Task<PagedData<ProductSales>> GetProductSales([FromQuery] GetProductSalesQuery request) => mediator.Send(request);

        [HttpPost("CreateNewProduct")]
        [SwaggerOperation("Create new product")]
        public Task CreateProduct([FromBody] CreateProductCommands request) => mediator.Send(request);

        [HttpPost("CreateNewProductSales")]
        [SwaggerOperation("Create new product sale")]
        public Task CreateProductsSale([FromBody] CreateProductSaleCommands request) => mediator.Send(request);
    }
}
