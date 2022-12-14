using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMMS.Application.DistributorManager.Interfaces;
using NMMS.Application.DistributorManager.Services;
using NMMS.Application.Tools.Options;
using NMMS.Common.Application.Tools.Extensions;
using NMMS.Common.FileManager.Tools.Extensions;
using System.Reflection;

namespace NMMS.Application.Tools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddCommonApplication(assembly);
            services.AddFileManager(configuration);
            services.Configure<FileExtensionOptions>(configuration.GetSection(nameof(FileExtensionOptions)));
            services.AddScoped<IDistributorLevelService, DistributorLevelService>();
        }
    }
}
