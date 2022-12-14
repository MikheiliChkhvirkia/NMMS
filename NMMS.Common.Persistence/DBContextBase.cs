using Microsoft.EntityFrameworkCore;
using NMMS.Common.Application.Interfaces.UniqueDateTime;
using NMMS.Common.Persistence.Contracts;
using System.Reflection;

namespace NMMS.Common.Persistence
{
    public class DBContextBase : DbContext
    {
        protected readonly IDateTimeService dateTimeService;
        public DBContextBase(DbContextOptions options, IDateTimeService dateTimeService)
            : base(options)
        {
            this.dateTimeService = dateTimeService;
        }

        protected void OnModelCreating(ModelBuilder modelBuilder, Assembly assembly)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<ITrackedEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.UpdateCreateCredentials(dateTimeService.Now);
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDeleteCredentials(dateTimeService.Now);
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}