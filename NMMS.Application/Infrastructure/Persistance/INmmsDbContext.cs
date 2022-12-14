using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Entities.Product;
using NMMS.Domain.Entities.ProductSale;
using File = NMMS.Domain.Entities.Files.File;

namespace NMMS.Application.Infrastructure.Persistance
{
    public interface INmmsDbContext
    {
        DbSet<Distributor> Distributors { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<IdentificationInformation> IdentificationInformations { get; set; }

        #region Enums
        DbSet<AddressTypes> AddressTypes { get; set; }
        DbSet<ContactTypes> ContactTypes { get; set; }
        DbSet<DocumentTypes> DocumentTypes { get; set; }
        DbSet<SexTypes> SexTypes { get; set; }
        DbSet<DistributorLevelTypes> DistributorLevelTypes { get; set; }
        #endregion

        #region ProductSales
        DbSet<ProductSales> ProductSales { get; set; }
        #endregion

        #region Products
        DbSet<Products> Products { get; set; }
        #endregion

        #region Files
        DbSet<File> Files { get; set; }
        #endregion

        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
