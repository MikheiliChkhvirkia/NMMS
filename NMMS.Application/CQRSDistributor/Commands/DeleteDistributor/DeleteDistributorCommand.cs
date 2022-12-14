using MediatR;

namespace NMMS.Application.CQRSDistributor.Commands.DeleteDistributor
{
    public class DeleteDistributorCommand : IRequest
    {
        public int Id { get; set; }
    }
}
