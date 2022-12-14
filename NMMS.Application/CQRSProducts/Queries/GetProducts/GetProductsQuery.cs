using MediatR;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSProducts.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<List<NamedData<int>>> { }
}
