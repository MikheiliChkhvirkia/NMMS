using Microsoft.EntityFrameworkCore;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Common.Application.Interfaces.UniqueDateTime;
using NMMS.Common.Persistence;
using NMMS.Domain.Entities.DistributorEntity;
using NMMS.Domain.Entities.Product;
using NMMS.Domain.Entities.ProductSale;
using System.Reflection;
using File = NMMS.Domain.Entities.Files.File;

namespace NMMS.Persistence.Entities
{
    public class NmmsDbContext : DBContextBase, INmmsDbContext
    {
        public NmmsDbContext(DbContextOptions<NmmsDbContext> options, IDateTimeService dateTimeService)
            : base(options, dateTimeService)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public virtual DbSet<Distributor> Distributors { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<IdentificationInformation> IdentificationInformations { get; set; }

        #region Enums
        public virtual DbSet<AddressTypes> AddressTypes { get; set; }
        public virtual DbSet<ContactTypes> ContactTypes { get; set; }
        public virtual DbSet<DocumentTypes> DocumentTypes { get; set; }
        public virtual DbSet<SexTypes> SexTypes { get; set; }
        public virtual DbSet<DistributorLevelTypes> DistributorLevelTypes { get; set; }

        #endregion

        #region ProductSales
        public virtual DbSet<ProductSales> ProductSales { get; set; }
        #endregion

        #region Products
        public virtual DbSet<Products> Products { get; set; }
        #endregion

        #region Files
        public virtual DbSet<File> Files { get; set; }


        #endregion

    }
}
