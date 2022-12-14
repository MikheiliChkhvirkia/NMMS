using MediatR;
using NMMS.Application.Enums;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSDistributor.Queries.GetDistributorsId
{
    public class GetDistributorsIdQueryHandler : IRequestHandler<GetDistributorsIdQuery, List<NamedData<int>>>
    {
        private readonly INmmsDbContext db;
        public GetDistributorsIdQueryHandler(INmmsDbContext db) =>
            this.db = db;

        public Task<List<NamedData<int>>> Handle(GetDistributorsIdQuery request, CancellationToken cancellationToken)
           => Task.FromResult(
               db.Distributors.Where(d => d.RecommendedDistributorCount < (int)RecommendedDistributorsEnum.RecommendedDistributorsMax 
                                       && d.RecommendedDistributorOverAllCount < (int)RecommendedDistributorsEnum.OverAllRecommendedDistributorsMax)
               .Select(d => new NamedData<int>
               {
                   Id = d.Id,
                   Name = $"{d.FirstName} {d.LastName} {d.IdentificationInformation.IdentityNumber}"
               }).ToList()
               );
    }
}
