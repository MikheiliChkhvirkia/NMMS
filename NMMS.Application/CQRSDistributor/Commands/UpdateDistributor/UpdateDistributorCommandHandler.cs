using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Tools.Extensions;

namespace NMMS.Application.CQRSDistributor.Commands.UpdateDistributor
{
    public class UpdateDistributorCommandHandler : IRequestHandler<UpdateDistributorCommand>
    {
        private readonly INmmsDbContext db;

        public UpdateDistributorCommandHandler(INmmsDbContext db)
            => this.db = db;

        public async Task<Unit> Handle(UpdateDistributorCommand request, CancellationToken cancellationToken)
        {
            var distributor = db.Distributors.FirstOrDefault(d => d.Id == request.Id && d.DeleteDate == null);
            distributor.EnsureNotNull();

            distributor.FirstName = request.Model.FirstName ?? distributor.FirstName;

            distributor.LastName = request.Model.LastName ?? distributor.LastName;

            distributor.BirthDate = request.Model.BirthDate != distributor.BirthDate ? request.Model.BirthDate : distributor.BirthDate;

            distributor.SexTypeId = request.Model.SexTypeId != 0 ?
                                    request.Model.SexTypeId :
                                    distributor.SexTypeId;

            distributor.RecommendatorDistributorId = request.Model.RecommendatorDistributorId != 0 ? 
                                                     request.Model.RecommendatorDistributorId : 
                                                     distributor.RecommendatorDistributorId;

            distributor.FileId = request.Model.FileId ?? distributor.FileId;

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
