using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Tools.Extensions;

namespace NMMS.Application.CQRSIdentification.Commands.UpdateIdentification
{
    public class UpdateIdentificationCommandHandler : IRequestHandler<UpdateIdentificationCommand>
    {
        private readonly INmmsDbContext db;

        public UpdateIdentificationCommandHandler(INmmsDbContext db)
            => this.db = db;

        public async Task<Unit> Handle(UpdateIdentificationCommand request, CancellationToken cancellationToken)
        {
            var result = db.IdentificationInformations.FirstOrDefault(x => x.Id == request.Id);
            result.EnsureNotNull();

            result.DocumentSeries = request.Model.DocumentSeries ?? result.DocumentSeries;
            result.IdentityNumber = request.Model.IdentityNumber ?? result.IdentityNumber;
            result.DocumentNumber = request.Model.DocumentNumber ?? result.DocumentNumber;
            result.ReleaseDate = request.Model.ReleaseDate != result.ReleaseDate ? request.Model.ReleaseDate : result.ReleaseDate;
            result.DocumentTerms = request.Model.DocumentTerms != result.DocumentTerms ? request.Model.DocumentTerms : result.DocumentTerms;
            result.IssuingCompany = request.Model.IssuingCompany ?? result.IssuingCompany;

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
