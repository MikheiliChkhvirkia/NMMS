using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NMMS.Common.ErrorHandling;

namespace NMMS.Common.ApiErrorHandling
{
    public class ApiProblemContext<TApiProblemDetails, TException>
        where TApiProblemDetails : ApiProblemDetails, new()
        where TException : Exception
    {
        public TApiProblemDetails ProblemDetails { get; internal set; }
        public HttpContext HttpContext { get; internal set; }
        public TException Exception { get; internal set; }
        public IServiceProvider ServiceProvider { get; internal set; }
        public ApiProblemLogging Logging { get; internal set; }


        public ApiProblemContext() { }

        public ApiProblemContext(TApiProblemDetails problemDetails,
            HttpContext httpContext,
            TException exception,
            IServiceProvider serviceProvider,
            ApiProblemLogging logging)
        {
            ProblemDetails = problemDetails;
            HttpContext = httpContext;
            Exception = exception;
            ServiceProvider = serviceProvider;
            Logging = logging;
        }
    }

    public class ApiProblemLogging
    {
        public bool EnableLogging { get; set; }
        public LogLevel LogLevel { get; set; }

        public ApiProblemLogging(bool enableLogging, LogLevel logLevel)
        {
            EnableLogging = enableLogging;
            LogLevel = logLevel;
        }
    }
}
