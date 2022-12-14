using NMMS.Application.Tools.Extensions;
using NMMS.Common.API.Tools.Extensions;
using NMMS.Infrastructure.Tools.Extensions;
using NMMS.Persistence.Tools.Extensions;
using System.Reflection;

namespace NMMS.API.Tools.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder, Assembly assembly)
        {
            builder.AddCommonApi(assembly);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddInfrastructure();
            builder.Services.AddPersistence(builder.Configuration);

        }
    }
}
