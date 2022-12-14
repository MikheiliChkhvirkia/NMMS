using NMMS.Application.DistributorManager.Interfaces;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Enums.DistributorEnum;

namespace NMMS.Application.DistributorManager.Services
{
    internal class DistributorLevelService : IDistributorLevelService
    {
        private readonly INmmsDbContext db;

        public DistributorLevelService(INmmsDbContext db)
            => this.db = db;
        public int DistributorRecomendatorsOverAllCount(int id)
        {
            var distributor = db.Distributors.First(d => d.Id == id);

            int parentDistributorId = distributor.DistributorLevelId != (int)DistributorLevelTypeEnum.LevelOne ? 
                                      FindParentDistributorId(distributor.Id, distributor.DistributorLevelId) :
                                      distributor.Id;

            return parentDistributorId;
        }

        private int FindParentDistributorId(int id, int levelId)
        {
            // ToDo: წაშლილები არ უნდა წამოიღოს. თუ წაშლის რა მოუვიდეს მის რეკომენდატორებს.
            //var distributors = db.Distributors.Where(x => x.DeleteDate == null);
            var distributors = db.Distributors.Select(x => x);

            Distributor distributor = new();

            for (int i = levelId; i > (int)DistributorLevelTypeEnum.LevelOne; i++ )
            {
                distributor = distributors.First(x => x.Id == id);
                if (distributor.DistributorLevelId == (int)DistributorLevelTypeEnum.LevelOne)
                    break;
                id = distributor.RecommendatorDistributorId;
            }

            return distributor.Id;
        }

    }
}
