namespace NMMS.Common.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string title, Exception innerException, int userId)
            : base("UnauthorizedException", title, $"User: {userId} is not authorized", innerException) { }

        public UnauthorizedException(string title)
            : base("UnauthorizedException", title) { }
    }
}
