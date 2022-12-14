using NMMS.Common.API.ErrorHandling;
using NMMS.Common.API.Tools.Extensions;
using NMMS.Common.ApiErrorHandling.Tools.Extensions;
using System.Reflection;

namespace NMMS.API.Tools.Extensions
{
    public static class ConfigureMiddlewaresExtensions
    {
        public static void ConfigureMiddlewares(this WebApplication app, Assembly assembly)
        {
            app.UseApiErrorHandling().AddExceptionHandler<ExceptionHandler>();
            app.UsePathBase();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(options => options
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
            );
            app.UseEndpoints(app.Environment.IsProduction());
            app.UseSwagger(assembly);
        }
    }
}
