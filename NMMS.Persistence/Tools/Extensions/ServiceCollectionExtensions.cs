using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMMS.Application.Infrastructure.Persistance;
using NMMS.Persistence.Entities;
using System.Reflection;

namespace NMMS.Persistence.Tools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<INmmsDbContext, NmmsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SQL"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                    sqlOptions.EnableRetryOnFailure();
                }));

            var serviceProvider = services.BuildServiceProvider();
            IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            serviceProvider.GetRequiredService<INmmsDbContext>().Database.Migrate();
        }
    }
}
