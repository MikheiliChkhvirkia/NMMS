using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NMMS.Common.ErrorHandling;

namespace NMMS.Common.ApiErrorHandling.Tools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiProblemDetailsFactory<TApiProblemDetails>(this IServiceCollection services,
            string? defaultErrorCode = null,
            string? validationErrorCode = null)
            where TApiProblemDetails : ApiProblemDetails, new()
        {
            services.AddSingleton<ProblemDetailsFactory>(serviceProvider =>
                new ApiProblemDetailsFactory<TApiProblemDetails>(
                    serviceProvider.GetService<IOptions<ApiBehaviorOptions>>(),
                    defaultErrorCode,
                    validationErrorCode)
                );

            return services;
        }

        public static IServiceCollection AddApiProblemDetailsFactory(
            this IServiceCollection services,
            string? defaultErrorCode = null,
            string? validationErrorCode = null)
        {
            return services.AddApiProblemDetailsFactory<ApiProblemDetails>(defaultErrorCode, validationErrorCode);
        }
    }
}
