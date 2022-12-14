using MediatR;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSAddress.Queries.GetAddressTypes
{
    public class GetAddressTypesQuery : IRequest<List<NamedData<int>>> { }
}
