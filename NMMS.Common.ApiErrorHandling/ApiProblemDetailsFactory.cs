using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using NMMS.Common.ApiErrorHandling.Tools.Options;
using NMMS.Common.ErrorHandling;
using System.Diagnostics;

namespace NMMS.Common.ApiErrorHandling
{
    public class ApiProblemDetailsFactory<TApiProblemDetails> : ProblemDetailsFactory
        where TApiProblemDetails : ApiProblemDetails, new()
    {
        private readonly ApiBehaviorOptions _options;

        private readonly string _defaultErrorCode;
        private readonly string _validationErrorCode;

        public ApiProblemDetailsFactory(IOptions<ApiBehaviorOptions> options,
            string? defaultErrorCode = null,
            string? validationErrorCode = null)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _defaultErrorCode = defaultErrorCode ?? DefaultErrorCodes.SystemError.Code;
            _validationErrorCode = validationErrorCode ?? DefaultErrorCodes.ValidationError.Code;
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            problemDetails.Extensions["code"] = _defaultErrorCode;

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            if (modelStateDictionary == null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            statusCode ??= StatusCodes.Status400BadRequest;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            if (title != null)
            {
                // For validation problem details, don't overwrite the default title with null.
                problemDetails.Title = title;
            }

            problemDetails.Extensions["code"] = _validationErrorCode;

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        public ApiProblemDetails CreateApiProblemDetails(
            HttpContext httpContext,
            string? code = null,
            int? statusCode = null,
            string? title = null,
            string? detail = null,
            string? instance = null,
            string? type = null,
            IDictionary<string, string[]>? errors = null
            )
        {
            var problemDetails = new TApiProblemDetails
            {
                Code = code ?? _defaultErrorCode,
                Status = statusCode ?? StatusCodes.Status500InternalServerError,
                Title = title,
                Detail = detail,
                Instance = instance,
                Type = type,
                Errors = errors
            };

            ApplyApiProblemDetailsDefaults(httpContext, problemDetails);

            return problemDetails;
        }

        public ApiProblemDetails CreateApiValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            string? code = null,
            int? statusCode = null,
            string? title = null,
            string? detail = null,
            string? instance = null,
            string? type = null)
        {
            var valProblems = new ValidationProblemDetails(modelStateDictionary);

            var problemDetails = new TApiProblemDetails
            {
                Code = code ?? _validationErrorCode,
                Status = statusCode ?? StatusCodes.Status400BadRequest,
                Title = title ?? valProblems.Title,
                Detail = detail,
                Instance = instance,
                Type = type,
                Errors = valProblems.Errors
            };

            ApplyApiProblemDetailsDefaults(httpContext, problemDetails);

            return problemDetails;
        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status ??= statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
            }

            problemDetails.Type ??= ErrorHandlingOptions<TApiProblemDetails>.Current.EnableErrorCodesEndpoint
                ? $"{ErrorHandlingOptions<TApiProblemDetails>.Current.ErrorCodesEndpointPath}#{problemDetails.Extensions["code"]}"
                : $"https://httpstatuses.com/{problemDetails.Status}";

            problemDetails.Title ??= DefaultErrorCodes.SystemError.Title;

            problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();
        }

        private void ApplyApiProblemDetailsDefaults(HttpContext httpContext, TApiProblemDetails apiProblemDetails)
        {
            if (_options.ClientErrorMapping.TryGetValue(apiProblemDetails.Status!.Value, out var clientErrorData))
            {
                apiProblemDetails.Title ??= clientErrorData.Title;
            }
            apiProblemDetails.Type ??= ErrorHandlingOptions<TApiProblemDetails>.Current.EnableErrorCodesEndpoint
                ? $"{ErrorHandlingOptions<TApiProblemDetails>.Current.ErrorCodesEndpointPath}#{apiProblemDetails.Code}"
                : $"https://httpstatuses.com/{apiProblemDetails.Status}";

            apiProblemDetails.Title ??= DefaultErrorCodes.SystemError.Title;
            apiProblemDetails.TraceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();
        }
    }
}
