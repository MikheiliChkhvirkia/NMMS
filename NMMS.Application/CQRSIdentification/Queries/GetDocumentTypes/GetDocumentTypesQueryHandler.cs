using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSIdentification.Queries.GetDocumentTypes
{
    public class GetDocumentTypesQueryHandler : IRequestHandler<GetDocumentTypesQuery, List<NamedData<int>>>
    {
        private readonly INmmsDbContext db;
        public GetDocumentTypesQueryHandler(INmmsDbContext db) =>
            this.db = db;

        public Task<List<NamedData<int>>> Handle(GetDocumentTypesQuery request, CancellationToken cancellationToken)
            => Task.FromResult(db.DocumentTypes.Select(x => new NamedData<int>
            {
                Id = x.Id,
                Name = x.Name
            }).ToList());


    }
}
