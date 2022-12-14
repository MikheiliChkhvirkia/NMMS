using Microsoft.AspNetCore.Builder;
using NMMS.Common.ApiErrorHandling.Tools.Options;
using NMMS.Common.ErrorHandling;

namespace NMMS.Common.ApiErrorHandling.Tools.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static ErrorHandlingBuilder<ApiProblemDetails> UseApiErrorHandling(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseApiErrorHandling<ApiProblemDetails>(null);
        }

        public static ErrorHandlingBuilder<ApiProblemDetails> UseApiErrorHandling(this IApplicationBuilder applicationBuilder,
            Action<ErrorHandlingOptions<ApiProblemDetails>> options)
        {
            return applicationBuilder.UseApiErrorHandling<ApiProblemDetails>(options);
        }

        public static ErrorHandlingBuilder<TApiProblemDetails> UseApiErrorHandling<TApiProblemDetails>(this IApplicationBuilder applicationBuilder)
            where TApiProblemDetails : ApiProblemDetails, new()
        {
            return applicationBuilder.UseApiErrorHandling<TApiProblemDetails>(null);
        }

        public static ErrorHandlingBuilder<TApiProblemDetails> UseApiErrorHandling<TApiProblemDetails>(this IApplicationBuilder applicationBuilder,
            Action<ErrorHandlingOptions<TApiProblemDetails>> options)
            where TApiProblemDetails : ApiProblemDetails, new()
        {
            options?.Invoke(ErrorHandlingOptions<TApiProblemDetails>.Current);

            applicationBuilder.UseMiddleware<ErrorHandlingMiddleware<TApiProblemDetails>>();

            return ErrorHandlingBuilder<TApiProblemDetails>.Current;
        }
    }
}
