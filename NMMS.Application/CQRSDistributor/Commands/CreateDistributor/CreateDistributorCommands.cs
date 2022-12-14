using AutoMapper;
using MediatR;
using NMMS.Common.Application.Tools.Mappings;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Application.CQRSDistributor.Commands.CreateDistributor
{
    public class CreateDistributorCommands : IRequest, IMap<Distributor>, IMap<IdentificationInformation>, IMap<Contact>, IMap<Address>
    {
        public DistributorModel DistributorModel { get; set; }
        public IdentificationInformationModel IdentificationInformationModel { get; set; }
        public ContactModel ContactModel { get; set; }
        public AddressModel AddressModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DistributorModel, Distributor>()
                .ForMember(dest => dest.Id, options => options.Ignore())
                .ForMember(dest => dest.CreateDate, options => options.Ignore())
                .ForMember(dest => dest.DeleteDate, options => options.Ignore());
            profile.CreateMap<IdentificationInformationModel, IdentificationInformation>();
            profile.CreateMap<ContactModel, Contact>();
            profile.CreateMap<AddressModel, Address>();
        }
    }

    public class DistributorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int RecommendatorDistributorId { get; set; }
        public int SexTypeId { get; set; }
        public Guid FileId { get; set; }
    }

    public class IdentificationInformationModel
    {
        public int DocumentTypeId { get; set; }

        public string? DocumentSeries { get; set; }

        public string? DocumentNumber { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime DocumentTerms { get; set; }

        public string IdentityNumber { get; set; }

        public string? IssuingCompany { get; set; }

    }

    public class ContactModel
    {
        public int ContactTypeId { get; set; }
        public string ContactInformation { get; set; }
    }

    public class AddressModel
    {
        public int AddressTypeId { get; set; }
        public string AddressInfo { get; set; }

    }
}
