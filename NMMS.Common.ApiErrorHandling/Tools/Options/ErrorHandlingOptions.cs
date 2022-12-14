using Microsoft.Extensions.Logging;
using NMMS.Common.ErrorHandling;

namespace NMMS.Common.ApiErrorHandling.Tools.Options
{
    public class ErrorHandlingOptions<TApiProblemDetails>
        where TApiProblemDetails : ApiProblemDetails, new()
    {
        private static ErrorHandlingOptions<TApiProblemDetails> _options;

        public static ErrorHandlingOptions<TApiProblemDetails> Current => _options ??= new ErrorHandlingOptions<TApiProblemDetails>();

        private ErrorHandlingOptions() { }

        /// <summary>
        /// Log problem details
        /// <value>(default: true)</value>
        /// </summary>
        public bool EnableLogging { get; set; } = true;

        /// <summary>
        /// Default logging severity level
        /// <value>(default: LogLevel.Error)</value>
        /// </summary>
        public LogLevel DefaultLogLevel { get; set; } = LogLevel.Error;

        [Obsolete("Please use CustomLogAction property")]
        public Action<ILogger, TApiProblemDetails, Exception> LogAction { get; set; }

        /// <summary>
        /// Override default log action
        /// </summary>
        public Action<ApiProblemLogContext<TApiProblemDetails>> CustomLogAction { get; set; }

        public bool EnableErrorCodesEndpoint { get; set; } = true;
        public string ErrorCodesEndpointPath { get; set; } = "/error-codes";
    }
}