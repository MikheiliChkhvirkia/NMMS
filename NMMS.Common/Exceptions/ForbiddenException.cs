namespace NMMS.Common.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException() : base("ForbiddenException", "Action not allowed", "User is not allowed to perform this action") { }
    }
}
