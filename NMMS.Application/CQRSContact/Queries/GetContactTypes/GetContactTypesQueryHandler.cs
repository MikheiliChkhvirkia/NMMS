using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSContact.Queries.GetContactTypes
{
    public class GetContactTypesQueryHandler : IRequestHandler<GetContactTypesQuery, List<NamedData<int>>>
    {
        private readonly INmmsDbContext db;
        public GetContactTypesQueryHandler(INmmsDbContext db) =>
            this.db = db;

        public Task<List<NamedData<int>>> Handle(GetContactTypesQuery request, CancellationToken cancellationToken)
        => Task.FromResult(db.ContactTypes.Select(x => new NamedData<int>
        {
            Id = x.Id,
            Name = x.Name
        }).ToList());
    }
}
