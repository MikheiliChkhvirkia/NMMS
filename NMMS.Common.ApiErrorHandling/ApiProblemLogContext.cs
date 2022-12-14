using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NMMS.Common.ErrorHandling;

namespace NMMS.Common.ApiErrorHandling
{
    public class ApiProblemLogContext<TApiProblemDetails>
        where TApiProblemDetails : ApiProblemDetails, new()
    {
        public TApiProblemDetails ProblemDetails { get; internal set; }
        public ILogger Logger { get; internal set; }
        public HttpContext HttpContext { get; internal set; }
        public Exception Exception { get; internal set; }
        public IServiceProvider ServiceProvider { get; internal set; }

        public LogLevel LogLevel { get; set; }

        public ApiProblemLogContext() { }

        public ApiProblemLogContext(TApiProblemDetails problemDetails, ILogger logger, HttpContext httpContext, Exception exception, IServiceProvider serviceProvider, LogLevel logLevel)
        {
            ProblemDetails = problemDetails;
            Logger = logger;
            HttpContext = httpContext;
            Exception = exception;
            ServiceProvider = serviceProvider;
            LogLevel = logLevel;
        }
    }
}