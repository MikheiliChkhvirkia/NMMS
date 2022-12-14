using AutoMapper;
using MediatR;
using NMMS.Application.DistributorManager.Interfaces;
using NMMS.Application.Enums;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Tools.Extensions;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Enums.DistributorEnum;

namespace NMMS.Application.CQRSDistributor.Commands.CreateDistributor
{
    public class CreateDistributorCommandsHandler : IRequestHandler<CreateDistributorCommands>
    {
        private readonly INmmsDbContext db;
        private readonly IMapper mapper;
        private readonly IDistributorLevelService distributorLevelService;

        public CreateDistributorCommandsHandler(INmmsDbContext db, IMapper mapper, IDistributorLevelService distributorLevelService)
        {
            this.db = db;
            this.mapper = mapper;
            this.distributorLevelService = distributorLevelService;
        }

        public async Task<Unit> Handle(CreateDistributorCommands request, CancellationToken cancellationToken)
        { 
            request.EnsureNotNull();

            if (CheckDistributors(request.IdentificationInformationModel.IdentityNumber))
                throw new Exception("Already Registered!");

            if (CheckImage(request.DistributorModel.FileId))
                throw new Exception("Image does not exist! (First Upload File)");

            if (CheckDistributorImage(request.DistributorModel.FileId))
                throw new Exception("Image is already assigned!");

            if (request.DistributorModel.RecommendatorDistributorId > 0)
            {
                Distributor? recommendedDistirubor = db.Distributors.FirstOrDefault(x => x.Id == request.DistributorModel.RecommendatorDistributorId);
                recommendedDistirubor.EnsureNotNull();
                var parentDistributorId = distributorLevelService.DistributorRecomendatorsOverAllCount(recommendedDistirubor.Id);
                Distributor parentDistributor = db.Distributors.First(x => x.Id == parentDistributorId);
                if (parentDistributor.RecommendedDistributorOverAllCount >= (int)RecommendedDistributorsEnum.OverAllRecommendedDistributorsMax)
                    throw new Exception("This recommendators parent recommendator can not register new user!");
                if (recommendedDistirubor?.RecommendedDistributorCount >= (int)RecommendedDistributorsEnum.RecommendedDistributorsMax)
                    throw new Exception("This recommendator can not register new user!");
                
                recommendedDistirubor.RecommendedDistributorCount += 1;
                parentDistributor.RecommendedDistributorOverAllCount += 1;
                
            }

            var identity = mapper.Map<IdentificationInformation>(request.IdentificationInformationModel);
            var contact = mapper.Map<Contact>(request.ContactModel);
            var address = mapper.Map<Address>(request.AddressModel);
            var distributor = new Distributor()
            {
                FirstName = request.DistributorModel.FirstName,
                LastName = request.DistributorModel.LastName,
                BirthDate = request.DistributorModel.BirthDate,
                SexTypeId = request.DistributorModel.SexTypeId,
                RecommendedDistributorCount = 0,
                RecommendatorDistributorId = request.DistributorModel.RecommendatorDistributorId,
                DistributorLevelId = request.DistributorModel.RecommendatorDistributorId == 0
                                ? (int)DistributorLevelTypeEnum.LevelOne
                                : (db.Distributors.FirstOrDefault(x => x.Id == request.DistributorModel.RecommendatorDistributorId).DistributorLevelId) + 1,
                FileId = request.DistributorModel.FileId,
                IdentificationInformation = identity,
                Contact = contact,
                Address = address
            };

            db.Distributors.Add(distributor);

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private bool CheckDistributors(string identityNumber)
            => db.IdentificationInformations.FirstOrDefault(i => i.IdentityNumber == identityNumber && i.DeleteDate == null) != null;
        private bool CheckImage(Guid fileId)
            => db.Files.FirstOrDefault(f => f.Id == fileId ) == null;
        private bool CheckDistributorImage(Guid fileId)
            => db.Distributors.FirstOrDefault(f => f.FileId == fileId && f.DeleteDate == null) != null;
    }
}
