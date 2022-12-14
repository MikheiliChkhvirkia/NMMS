using MediatR;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Application;
using NMMS.Common.Application.Tools.Extensions;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Application.CQRSDistributor.Queries.GetAllDistributors
{
    public class GetAllDistributorsQueryHandler : IRequestHandler<GetAllDistributorsQuery, PagedData<Distributor>>
    {
        private readonly INmmsDbContext db;
        public GetAllDistributorsQueryHandler(INmmsDbContext db) =>
            this.db = db;

        public Task<PagedData<Distributor>> Handle(GetAllDistributorsQuery request, CancellationToken cancellationToken)
        => Task.FromResult(db.Distributors
            .Where(d => d.DeleteDate == null)
            .Select(x => new Distributor
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate,
                RecommendedDistributorCount = x.RecommendedDistributorCount,
                DistributorLevelId = x.DistributorLevelId,
                DistributorLevelType = x.DistributorLevelType,
                SexTypeId = x.SexTypeId,
                SexTypes = x.SexTypes,
                FileId = x.FileId,
                File = x.File,
                IdentificationInformationId = x.IdentificationInformationId,
                IdentificationInformation = new IdentificationInformation
                {
                    DocumentTypeId = x.IdentificationInformation.DocumentTypeId,
                    DocumentSeries = x.IdentificationInformation.DocumentSeries,
                    DocumentNumber = x.IdentificationInformation.DocumentNumber,
                    ReleaseDate = x.IdentificationInformation.ReleaseDate,
                    DocumentTerms = x.IdentificationInformation.DocumentTerms,
                    IdentityNumber = x.IdentificationInformation.IdentityNumber,
                    IssuingCompany = x.IdentificationInformation.IssuingCompany,
                    DocumentTypes = x.IdentificationInformation.DocumentTypes,
                },
                ContactId = x.ContactId,
                Contact = new Contact
                {
                    ContactTypeId = x.Contact.ContactTypeId,
                    ContactInformation = x.Contact.ContactInformation,
                    ContactType = x.Contact.ContactType
                },
                AddressId = x.AddressId,
                Address = new Address
                {
                    AddressTypeId = x.Address.AddressTypeId,
                    AddressInfo = x.Address.AddressInfo,
                    AddressType = x.Address.AddressType,
                },
                RecommendatorDistributorId = x.RecommendatorDistributorId,
            })
            .ToPagedData(request.Page,request.Offset));

    }
}
