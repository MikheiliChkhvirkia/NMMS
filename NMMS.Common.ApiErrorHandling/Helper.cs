using NMMS.Common.ApiErrorHandling.Tools.Options;
using NMMS.Common.ErrorHandling;

namespace NMMS.Common.ApiErrorHandling
{
    internal static class Helper
    {
        internal static string GetDefaultErrorType<TApiProblemDetails>(string errorCode, int statusCode)
            where TApiProblemDetails : ApiProblemDetails, new()
        {
            return ErrorHandlingOptions<TApiProblemDetails>.Current.EnableErrorCodesEndpoint
                ? $"{ErrorHandlingOptions<TApiProblemDetails>.Current.ErrorCodesEndpointPath}#{errorCode}"
                : $"https://httpstatuses.com/{statusCode}";
        }
    }
}
