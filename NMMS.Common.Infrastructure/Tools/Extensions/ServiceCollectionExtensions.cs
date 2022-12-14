using Microsoft.Extensions.DependencyInjection;
using NMMS.Common.Application.Interfaces.UniqueDateTime;
using NMMS.Common.Infrastructure.Services.UniqueDateTime;

namespace NMMS.Common.Infrastructure.Tools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeService, DateTimeService>();
        }
    }
}
