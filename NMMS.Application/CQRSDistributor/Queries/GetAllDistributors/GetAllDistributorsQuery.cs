using MediatR;
using NMMS.Common.Application;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Application.CQRSDistributor.Queries.GetAllDistributors
{
    public class GetAllDistributorsQuery : IRequest<PagedData<Distributor>> 
    {
        public int Page { get; set; } = 1;
        public int Offset { get; set; } = 10;
    }
}
