using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSProducts.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<NamedData<int>>>
    {
        private readonly INmmsDbContext db;
        public GetProductsQueryHandler(INmmsDbContext db) =>
            this.db = db;

        public Task<List<NamedData<int>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            => Task.FromResult(db.Products.Select(p => new NamedData<int>
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList());
    }
}
