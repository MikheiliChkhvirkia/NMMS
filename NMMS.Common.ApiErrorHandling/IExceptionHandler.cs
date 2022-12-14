using NMMS.Common.ErrorHandling;

namespace NMMS.Common.ApiErrorHandling
{
    public interface IExceptionHandlerBase<TApiProblemDetails>
        where TApiProblemDetails : ApiProblemDetails, new()
    {

    }

    public interface IExceptionHandler<TApiProblemDetails, TException> : IExceptionHandlerBase<TApiProblemDetails>
        where TApiProblemDetails : ApiProblemDetails, new()
        where TException : Exception
    {
        void Handle(ApiProblemContext<TApiProblemDetails, TException> ctx);
    }

    public interface IExceptionHandler<TException> : IExceptionHandler<ApiProblemDetails, TException>
        where TException : Exception
    {
    }
}
