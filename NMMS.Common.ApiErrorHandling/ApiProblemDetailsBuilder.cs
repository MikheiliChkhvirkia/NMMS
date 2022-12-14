using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NMMS.Common.ApiErrorHandling.Tools.Options;
using NMMS.Common.ErrorHandling;
using System.Diagnostics;
using System.Reflection;

namespace NMMS.Common.ApiErrorHandling
{
    public class ApiProblemDetailsBuilder<TApiProblemDetails>
        where TApiProblemDetails : ApiProblemDetails, new()
    {
        private static ApiProblemDetailsBuilder<TApiProblemDetails> _apiProblemDetailsBuilder;

        private readonly Dictionary<Type, Action<ApiProblemContext<TApiProblemDetails, Exception>>> _builderActions;

        private readonly Dictionary<Type, object> _exceptionHandlers;

        internal static ApiProblemDetailsBuilder<TApiProblemDetails> Current
        {
            get
            {
                if (_apiProblemDetailsBuilder != null)
                    return _apiProblemDetailsBuilder;

                _apiProblemDetailsBuilder = new ApiProblemDetailsBuilder<TApiProblemDetails>();

                return _apiProblemDetailsBuilder;
            }
        }

        private ApiProblemDetailsBuilder()
        {
            _builderActions = new Dictionary<Type, Action<ApiProblemContext<TApiProblemDetails, Exception>>>();
            _exceptionHandlers = new Dictionary<Type, object>();

            When<Exception>(context => { });
        }

        public ApiProblemDetailsBuilder<TApiProblemDetails> AddExceptionHandler<TExceptionHandler>()
            where TExceptionHandler : IExceptionHandlerBase<TApiProblemDetails>
        {
            var errorHandlerType = typeof(TExceptionHandler);

            var methods = errorHandlerType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.Name == nameof(IExceptionHandler<TApiProblemDetails, Exception>.Handle)
                    && x.GetParameters() is { } parameters
                    && parameters.Length == 1
                    && parameters.First().ParameterType is { } paramType
                    && paramType.IsGenericType
                    && paramType.GetGenericTypeDefinition() is { } gtd
                    && gtd == typeof(ApiProblemContext<,>))
                .ToList();

            var errorHandlerObject = Activator.CreateInstance(errorHandlerType);

            foreach (var method in methods)
            {
                var exceptionType = method.GetParameters().First().ParameterType.GetGenericArguments()[1];
                var problemContextType = typeof(ApiProblemContext<,>).MakeGenericType(typeof(TApiProblemDetails), exceptionType);

                _builderActions[exceptionType] = BuilderAction;
                _exceptionHandlers[exceptionType] = errorHandlerObject;

                void BuilderAction(ApiProblemContext<TApiProblemDetails, Exception> context)
                {
                    var errorHandler = _exceptionHandlers[exceptionType];
                    method.Invoke(errorHandler,
                        new object[]
                        {
                            Activator.CreateInstance(problemContextType, new object[]
                            {
                                context.ProblemDetails,
                                context.HttpContext,
                                context.Exception,
                                context.ServiceProvider,
                                context.Logging
                            })
                        });
                };
            }

            return this;
        }

        public ApiProblemDetailsBuilder<TApiProblemDetails> When<TException>(Action<ApiProblemContext<TApiProblemDetails, TException>> builderAction)
            where TException : Exception
        {
            var exceptionType = typeof(TException);

            _builderActions[exceptionType] = BuilderAction;

            void BuilderAction(ApiProblemContext<TApiProblemDetails, Exception> context) => builderAction(new ApiProblemContext<TApiProblemDetails, TException>
            (
                context.ProblemDetails,
                context.HttpContext,
                (TException)context.Exception,
                context.ServiceProvider,
                context.Logging
            ));

            return this;
        }

        public ApiProblemContext<TApiProblemDetails, Exception> BuildProblemDetails(HttpContext context, Exception exception, IServiceProvider serviceProvider)
        {
            var problemDetails = new TApiProblemDetails
            {
                TraceId = Activity.Current?.Id ?? context?.TraceIdentifier ?? Guid.NewGuid().ToString(),
                Status = StatusCodes.Status500InternalServerError,
                Code = DefaultErrorCodes.SystemError.Code,
                Title = DefaultErrorCodes.SystemError.Title
            };

            var problemContext = new ApiProblemContext<TApiProblemDetails, Exception>(problemDetails, context, exception, serviceProvider,
                new ApiProblemLogging(ErrorHandlingOptions<TApiProblemDetails>.Current.EnableLogging,
                    ErrorHandlingOptions<TApiProblemDetails>.Current.DefaultLogLevel));

            var builderAction = GetBuilderAction(exception.GetType());
            builderAction?.Invoke(problemContext);

            var apiBehaviorOptions = serviceProvider.GetService<IOptions<ApiBehaviorOptions>>();
            if (apiBehaviorOptions?.Value != null &&
                apiBehaviorOptions.Value.ClientErrorMapping.TryGetValue(problemDetails.Status.Value,
                    out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
            }

            problemDetails.Status ??= StatusCodes.Status500InternalServerError;
            problemDetails.Type ??= Helper.GetDefaultErrorType<TApiProblemDetails>(problemDetails.Code,
                problemDetails.Status.Value);


            return problemContext;
        }

        private Action<ApiProblemContext<TApiProblemDetails, Exception>> GetBuilderAction(Type exceptionType)
        {
            if (_builderActions.ContainsKey(exceptionType))
            {
                return _builderActions[exceptionType];
            }

            if (exceptionType != typeof(Exception))
            {
                return GetBuilderAction(exceptionType.BaseType);
            }

            return null;
        }
    }
}
