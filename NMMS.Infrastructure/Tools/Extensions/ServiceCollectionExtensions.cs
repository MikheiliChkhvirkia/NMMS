using Microsoft.Extensions.DependencyInjection;
using NMMS.Common.Infrastructure.Tools.Extensions;

namespace NMMS.Infrastructure.Tools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddCommonInfrastructure();
        }
    }
}
