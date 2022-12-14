using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMMS.Common.FileManager.Interfaces;
using NMMS.Common.FileManager.Services;
using NMMS.Common.FileManager.Tools.Options;

namespace NMMS.Common.FileManager.Tools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFileManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileManagerOptions>(configuration.GetSection(nameof(FileManagerOptions)));

            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IFileDeleteService, FileDeleteService>();
            services.AddScoped<IFileFetchService, FileFetchService>();
        }
    }
}
