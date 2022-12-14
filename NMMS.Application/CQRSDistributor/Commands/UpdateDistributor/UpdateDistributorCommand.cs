using MediatR;

namespace NMMS.Application.CQRSDistributor.Commands.UpdateDistributor
{
    public class UpdateDistributorCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateDistributorCommandModel Model { get; set; }

    }

    public class UpdateDistributorCommandModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int SexTypeId { get; set; }

        public int RecommendatorDistributorId { get; set; }

        public Guid? FileId { get; set; }
    }
}
