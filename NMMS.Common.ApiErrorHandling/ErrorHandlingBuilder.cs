using NMMS.Common.ErrorHandling;

namespace NMMS.Common.ApiErrorHandling
{
    public class ErrorHandlingBuilder<TApiProblemDetails>
        where TApiProblemDetails : ApiProblemDetails, new()
    {
        private static ErrorHandlingBuilder<TApiProblemDetails> _builder;

        internal static ErrorHandlingBuilder<TApiProblemDetails> Current
        {
            get
            {
                if (_builder != null)
                    return _builder;

                _builder = new ErrorHandlingBuilder<TApiProblemDetails>();
                _builder.AddErrorCodeDescriptions(DefaultErrorCodes.DefaultErrorCodesList);

                return _builder;
            }
        }

        public ErrorHandlingBuilder<TApiProblemDetails> AddExceptionHandler<TExceptionHandler>()
            where TExceptionHandler : IExceptionHandlerBase<TApiProblemDetails>
        {
            ApiProblemDetailsBuilder<TApiProblemDetails>.Current.AddExceptionHandler<TExceptionHandler>();
            return this;
        }

        public ErrorHandlingBuilder<TApiProblemDetails> When<TException>(Action<ApiProblemContext<TApiProblemDetails, TException>> builderAction)
            where TException : Exception
        {
            ApiProblemDetailsBuilder<TApiProblemDetails>.Current.When(builderAction);
            return this;
        }

        public ErrorHandlingBuilder<TApiProblemDetails> AddErrorCodeDescriptions(List<ErrorCodeDescription> errorCodes)
        {
            if (errorCodes == null || errorCodes.Count == 0)
                return this;

            ErrorCodeDescriptions.AddRange(errorCodes);

            return this;
        }

        public ErrorHandlingBuilder<TApiProblemDetails> AddErrorCodeDescription(ErrorCodeDescription errorCode)
        {
            if (errorCode == null)
                return this;

            ErrorCodeDescriptions.Add(errorCode);

            return this;
        }
    }
}
