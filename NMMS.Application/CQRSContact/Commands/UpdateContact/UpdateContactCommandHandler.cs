using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Tools.Extensions;

namespace NMMS.Application.CQRSContact.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
    {
        private readonly INmmsDbContext db;

        public UpdateContactCommandHandler(INmmsDbContext db)
            => this.db = db;

        public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var result = db.Contacts.FirstOrDefault(x => x.Id == request.Id);
            result.EnsureNotNull();

            result.ContactTypeId = request.Model.ContactTypeId != null ? request.Model.ContactTypeId : result.ContactTypeId;
            result.ContactInformation = request.Model.ContactInformation ?? result.ContactInformation;

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
