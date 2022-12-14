using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Tools.Extensions;

namespace NMMS.Application.CQRSAddress.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand>
    {
        private readonly INmmsDbContext db;

        public UpdateAddressCommandHandler(INmmsDbContext db)
            => this.db = db;


        public async Task<Unit> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var result = db.Addresses.FirstOrDefault(x => x.Id == request.Id);
            result.EnsureNotNull();

            result.AddressTypeId = request.Model.AddressTypeId != null ? request.Model.AddressTypeId : result.AddressTypeId;
            result.AddressInfo = request.Model.AddressInfo ?? result.AddressInfo;

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
