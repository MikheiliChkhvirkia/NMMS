using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NMMS.Common.ApiErrorHandling;
using NMMS.Common.ErrorHandling;
using NMMS.Common.ErrorHandling.NewtonsoftJson;
using NMMS.Common.Exceptions;
using NMMS.Common.Tools.Extensions;
using RestEase;
using System.Collections;

namespace NMMS.Common.API.ErrorHandling
{
    public class ExceptionHandler : IExceptionHandler<Exception>,
         IExceptionHandler<AppException>,
         IExceptionHandler<AppValidationException>,
         IExceptionHandler<HttpException>,
         IExceptionHandler<ObjectNotFoundException>,
         IExceptionHandler<ApiException>,
         IExceptionHandler<UnauthorizedException>,
         IExceptionHandler<ForbiddenException>
    {
        private static readonly ApiProblemDetailsConverter apiProblemDetailsConverter = new();
        public void Handle(ApiProblemContext<ApiProblemDetails, Exception> ctx)
        {
            ctx.ProblemDetails.Type = "AppException";
            ctx.ProblemDetails.Title = ctx.Exception.Message;
            ctx.ProblemDetails.Status = StatusCodes.Status500InternalServerError;
            ctx.ProblemDetails.Detail = ctx.Exception.Message;

            foreach (DictionaryEntry item in ctx.Exception.Data)
            {
                ctx.ProblemDetails.Extensions.Add(new KeyValuePair<string, object>(item.Key.ToString().ToCamelCase(), item.Value));
            }

            ctx.ProblemDetails.Code = "AppException";
            ctx.Logging.LogLevel = LogLevel.Error;
        }

        public void Handle(ApiProblemContext<ApiProblemDetails, AppException> ctx)
        {
            ctx.ProblemDetails.Type = "AppException";
            ctx.ProblemDetails.Title = ctx.Exception.Title;
            ctx.ProblemDetails.Status = StatusCodes.Status400BadRequest;
            ctx.ProblemDetails.Detail = ctx.Exception.Message;
            ctx.ProblemDetails.Code = ctx.Exception.Code;
            ctx.Logging.LogLevel = LogLevel.Error;
        }

        public void Handle(ApiProblemContext<ApiProblemDetails, AppValidationException> ctx)
        {
            ctx.ProblemDetails.Type = "AppValidationException";
            ctx.ProblemDetails.Title = ctx.Exception.Title;
            ctx.ProblemDetails.Status = StatusCodes.Status400BadRequest;
            ctx.ProblemDetails.Detail = ctx.Exception.Message;
            ctx.ProblemDetails.Extensions["validationFailures"] = ctx.Exception.Failures;
            ctx.ProblemDetails.Code = ctx.Exception.Code;
            ctx.Logging.LogLevel = LogLevel.Error;
        }

        public void Handle(ApiProblemContext<ApiProblemDetails, HttpException> ctx)
        {
            ctx.ProblemDetails.Type = "HttpException";
            ctx.ProblemDetails.Title = ctx.Exception.Title;
            ctx.ProblemDetails.Status = StatusCodes.Status502BadGateway;
            ctx.ProblemDetails.Detail = ctx.Exception.Message;
            ctx.ProblemDetails.Code = ctx.Exception.Code;
            ctx.ProblemDetails.Extensions["endpoint"] = ctx.Exception.Endpoint;
            ctx.Logging.LogLevel = LogLevel.Error;
        }

        public void Handle(ApiProblemContext<ApiProblemDetails, ObjectNotFoundException> ctx)
        {
            ctx.ProblemDetails.Type = "ObjectNotFoundException";
            ctx.ProblemDetails.Title = ctx.Exception.Title;
            ctx.ProblemDetails.Status = StatusCodes.Status404NotFound;
            ctx.ProblemDetails.Detail = ctx.Exception.Message;
            ctx.ProblemDetails.Code = ctx.Exception.Code;
            ctx.Logging.LogLevel = LogLevel.Error;
        }

        public void Handle(ApiProblemContext<ApiProblemDetails, ApiException> ctx)
        {
            ApiProblemDetails problemDetails = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(ctx.Exception.Content))
                {
                    problemDetails = JsonConvert.DeserializeObject<ApiProblemDetails>(ctx.Exception.Content!, apiProblemDetailsConverter);
                }
            }
            catch { }

            if (problemDetails != null)
            {
                ctx.ProblemDetails.Type = problemDetails.Type;
                ctx.ProblemDetails.Title = problemDetails.Title;
                ctx.ProblemDetails.Status = problemDetails.Status;
                ctx.ProblemDetails.Detail = problemDetails.Detail;
                ctx.ProblemDetails.Code = problemDetails.Code;
                ctx.Logging.LogLevel = LogLevel.Error;
            }
            else
            {
                ctx.ProblemDetails.Type = "HttpException";
                ctx.ProblemDetails.Title = DefaultErrorCodes.SystemError.Title;
                ctx.ProblemDetails.Status = StatusCodes.Status500InternalServerError;
                ctx.ProblemDetails.Detail = DefaultErrorCodes.SystemError.Description;
                ctx.ProblemDetails.Code = "HttpException";
                ctx.Logging.LogLevel = LogLevel.Error;
                ctx.Logging.EnableLogging = false;
                ctx.ServiceProvider.GetRequiredService<ILogger<ExceptionHandler>>().LogError(ctx.Exception,
                    "Error detail, request URL: {RequestUrl}, ResponseBody: {ResponseBody}",
                    ctx.Exception.RequestUri?.ToString(),
                    ctx.Exception.Content);
            }
        }

        public void Handle(ApiProblemContext<ApiProblemDetails, UnauthorizedException> ctx)
        {
            ctx.ProblemDetails.Type = "UnauthorizedException";
            ctx.ProblemDetails.Title = ctx.Exception.Title;
            ctx.ProblemDetails.Status = StatusCodes.Status401Unauthorized;
            ctx.ProblemDetails.Detail = ctx.Exception.Message;
            ctx.ProblemDetails.Code = ctx.Exception.Code;
            ctx.Logging.LogLevel = LogLevel.Error;
        }

        public void Handle(ApiProblemContext<ApiProblemDetails, ForbiddenException> ctx)
        {
            ctx.ProblemDetails.Type = "ForbiddenException";
            ctx.ProblemDetails.Title = ctx.Exception.Title;
            ctx.ProblemDetails.Status = StatusCodes.Status403Forbidden;
            ctx.ProblemDetails.Detail = ctx.Exception.Message;
            ctx.ProblemDetails.Code = ctx.Exception.Code;
            ctx.Logging.LogLevel = LogLevel.Error;
        }
    }
}
