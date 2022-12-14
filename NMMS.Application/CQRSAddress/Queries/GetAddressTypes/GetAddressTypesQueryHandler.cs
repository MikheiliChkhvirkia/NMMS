using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSAddress.Queries.GetAddressTypes
{
    public class GetAddressTypesQueryHandler : IRequestHandler<GetAddressTypesQuery, List<NamedData<int>>>
    {
        private readonly INmmsDbContext db;
        public GetAddressTypesQueryHandler(INmmsDbContext db) =>
            this.db = db;

        public Task<List<NamedData<int>>> Handle(GetAddressTypesQuery request, CancellationToken cancellationToken)
        => Task.FromResult(db.AddressTypes.Select(x => new NamedData<int>
        {
            Id = x.Id,
            Name = x.Name
        }).ToList());
    }
}
