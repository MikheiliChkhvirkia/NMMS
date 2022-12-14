using MediatR;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSDistributor.Queries.GetDistributorsId
{
    public class GetDistributorsIdQuery : IRequest<List<NamedData<int>>> { }
}
