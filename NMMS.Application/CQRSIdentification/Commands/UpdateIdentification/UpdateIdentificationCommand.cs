using MediatR;

namespace NMMS.Application.CQRSIdentification.Commands.UpdateIdentification
{
    public class UpdateIdentificationCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateIdentificationCommandModel Model { get; set; }

    }
    public class UpdateIdentificationCommandModel
    {
        public string? DocumentSeries { get; set; }

        public string? DocumentNumber { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime DocumentTerms { get; set; }

        public string IdentityNumber { get; set; }

        public string? IssuingCompany { get; set; }
    }
}
