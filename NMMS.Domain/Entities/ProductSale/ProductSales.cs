using NMMS.Common.Domain.Entities;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Entities.Product;

namespace NMMS.Domain.Entities.ProductSale
{
    public class ProductSales : BaseEntity<int>
    {
        public int DistributorId { get; set; }
        public DateTime SellDate { get; set; }
        public int ProductsId { get; set; }
        public double Price { get; set; }
        public double UnitPrice { get; set; }
        public double OverAllPrice { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual Products Products { get; set; }
    }
}
