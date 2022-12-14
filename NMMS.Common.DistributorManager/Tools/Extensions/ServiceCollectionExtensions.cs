using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMMS.Common.DistributorManager.Interfaces;
using NMMS.Common.DistributorManager.Services;

namespace NMMS.Common.DistributorManager.Tools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDistributorManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDistributorManagerService, DistributorManagerService>();
        }
    }
}
