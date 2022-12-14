using MediatR;

namespace NMMS.Application.CQRSAddress.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateDistributorsCommandModel Model { get; set; }

    }
    public class UpdateDistributorsCommandModel
    {
        public int AddressTypeId { get; set; }
        public string AddressInfo { get; set; }
    }
}
