using MediatR;

namespace NMMS.Application.CQRSContact.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateContactCommandModel Model { get; set; }

    }
    public class UpdateContactCommandModel
    {
        public int ContactTypeId { get; set; }
        public string ContactInformation { get; set; }
    }
}
