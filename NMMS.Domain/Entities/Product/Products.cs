using NMMS.Common.Domain.Entities;
using NMMS.Domain.Entities.ProductSale;

namespace NMMS.Domain.Entities.Product
{
    public class Products : BaseEntity<int>
    {
        public Guid Code { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }

        public virtual ICollection<ProductSales> ProductSales { get; set; }
    }
}
