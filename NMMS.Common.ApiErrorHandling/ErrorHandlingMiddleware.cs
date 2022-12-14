using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NMMS.Common.ApiErrorHandling.Tools.Options;
using NMMS.Common.ErrorHandling;
using System.Diagnostics;
using System.Text.Json;

namespace NMMS.Common.ApiErrorHandling
{
    public class ErrorHandlingMiddleware<TApiProblemDetails>
        where TApiProblemDetails : ApiProblemDetails, new()
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware<TApiProblemDetails>> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ErrorHandlingMiddleware(RequestDelegate next,
            ILogger<ErrorHandlingMiddleware<TApiProblemDetails>> logger,
            IServiceProvider serviceProvider)
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var ctx = ApiProblemDetailsBuilder<TApiProblemDetails>.Current.BuildProblemDetails(context, exception, _serviceProvider);

            if (ctx.Logging.EnableLogging)
                LogError(ctx);

            //Send response
            var result = JsonSerializer.Serialize(ctx.ProblemDetails, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });
            context.Response.Clear();
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = ctx.ProblemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(result);
        }

        private void LogError(ApiProblemContext<TApiProblemDetails, Exception> ctx)
        {
            if (ErrorHandlingOptions<TApiProblemDetails>.Current.CustomLogAction != null)
            {
                ErrorHandlingOptions<TApiProblemDetails>.Current.CustomLogAction.Invoke(new ApiProblemLogContext<TApiProblemDetails>(
                    ctx.ProblemDetails,
                    _logger,
                    ctx.HttpContext,
                    ctx.Exception,
                    _serviceProvider,
                    ctx.Logging.LogLevel
                    ));
            }
            else if (ErrorHandlingOptions<TApiProblemDetails>.Current.LogAction != null)
            {
                ErrorHandlingOptions<TApiProblemDetails>.Current.LogAction.Invoke(_logger, ctx.ProblemDetails, ctx.Exception);
            }
            else
            {
                var logContext = new Dictionary<string, object>
                {
                    {"TraceId", ctx.ProblemDetails.TraceId},
                    {"ErrorType", ctx.ProblemDetails.Type},
                    {"ErrorTitle", ctx.ProblemDetails.Title},
                    {"ErrorCode", ctx.ProblemDetails.Code},
                    {"ErrorDetail", ctx.ProblemDetails.Detail},
                    {"ErrorInstance", ctx.ProblemDetails.Instance},
                    {"HttpStatusCode", ctx.ProblemDetails.Status}
                };
                if (Activity.Current != null)
                {
                    if (!string.IsNullOrEmpty(Activity.Current.RootId)
                        && Activity.Current.RootId.Any(x => x != '0'))
                        logContext["TraceRootId"] = Activity.Current.RootId;

                    if (!string.IsNullOrEmpty(Activity.Current.ParentId)
                        && Activity.Current.ParentId.Any(x => x != '0'))
                        logContext["TraceParentId"] = Activity.Current.ParentId;

                    var traceSpanId = Activity.Current.SpanId.ToString();
                    if (!string.IsNullOrEmpty(traceSpanId)
                        && traceSpanId.Any(x => x != '0'))
                        logContext["TraceSpanId"] = traceSpanId;

                    var traceParentSpanId = Activity.Current.ParentSpanId.ToString();
                    if (!string.IsNullOrEmpty(traceParentSpanId)
                        && traceParentSpanId.Any(x => x != '0'))
                        logContext["TraceParentSpanId"] = traceParentSpanId;
                }
                if (ctx.ProblemDetails.Extensions != null && ctx.ProblemDetails.Extensions.Count > 0)
                    logContext = logContext.Concat(ctx.ProblemDetails.Extensions).ToDictionary(pair => pair.Key, pair => pair.Value);

                using (_logger.BeginScope(logContext))
                {
                    if (ctx.Logging.LogLevel > LogLevel.Information)
                    {

                        _logger.Log(ctx.Logging.LogLevel, ctx.Exception, "Api Error Occurred, TraceId: {TraceId}, Code: {ErrorCode}, Title: {ErrorTitle}, Detail: {ErrorDetail}",
                            ctx.ProblemDetails.TraceId, ctx.ProblemDetails.Code, ctx.ProblemDetails.Title, ctx.ProblemDetails.Detail);
                    }
                    else
                    {
                        _logger.Log(ctx.Logging.LogLevel, "Api Error Occurred, TraceId: {TraceId}, Code: {ErrorCode}, Title: {ErrorTitle}, Detail: {ErrorDetail}",
                            ctx.ProblemDetails.TraceId, ctx.ProblemDetails.Code, ctx.ProblemDetails.Title, ctx.ProblemDetails.Detail);
                    }
                }
            }
        }
    }
}
