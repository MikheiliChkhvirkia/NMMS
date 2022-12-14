using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using NMMS.Common.API.Swagger;
using NMMS.Common.ApiErrorHandling.Tools.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace NMMS.Common.API.Tools.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddCommonApi(this WebApplicationBuilder builder, Assembly assembly)
        {
            builder.Services.AddApiProblemDetailsFactory()
                .AddControllers()
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddLocalization();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHealthChecks(builder.Configuration, assembly);
            builder.Services.AddApiVersioningOptions();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();
        }

        #region Private
        private static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            IHealthChecksBuilder builder = services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
            string connectionString = configuration.GetConnectionString("SQL");

            if (connectionString != null)
            {
                builder.AddSqlServer(connectionString, name: $"{assembly.GetName().Name}_SQL");
            }
        }
        private static void AddApiVersioningOptions(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                options.CustomSchemaIds(type => type.ToString());

            });
        }
        #endregion
    }
}
