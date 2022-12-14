using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Tools.Extensions;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Application.CQRSDistributor.Commands.DeleteDistributor
{
    public class DeleteDistributorCommandHandler : IRequestHandler<DeleteDistributorCommand>
    {
        private readonly INmmsDbContext db;

        public DeleteDistributorCommandHandler(INmmsDbContext db)
            => this.db = db;

        public async Task<Unit> Handle(DeleteDistributorCommand request, CancellationToken cancellationToken)
        {
            var distributor = db.Distributors.FirstOrDefault(d => d.Id == request.Id);
            distributor.EnsureNotNull();

            var contact = db.Contacts.FirstOrDefault(x => x.Id == distributor.ContactId && x.DeleteDate == null);
            var address = db.Addresses.FirstOrDefault(x => x.Id == distributor.AddressId && x.DeleteDate == null);
            var identity = db.IdentificationInformations.FirstOrDefault(x => x.Id == distributor.IdentificationInformationId && x.DeleteDate == null);

            var recomendator = db.Distributors.FirstOrDefault(d => d.Id == distributor.RecommendatorDistributorId);
            recomendator.RecommendedDistributorCount = recomendator.RecommendedDistributorCount > 0 ? 
                                                       recomendator.RecommendedDistributorCount - 1 : 
                                                       recomendator.RecommendedDistributorCount;

            distributor.DeleteDate = DateTime.Now;
            address.DeleteDate = DateTime.Now;
            contact.DeleteDate = DateTime.Now;
            identity.DeleteDate = DateTime.Now;

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
