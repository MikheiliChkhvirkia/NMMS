namespace NMMS.Common.ErrorHandling
{
    public static class DefaultErrorCodes
    {
        public static ErrorCodeDescription SystemError = new(nameof(SystemError),
            "An error occurred while processing your request");

        public static ErrorCodeDescription ValidationError = new(nameof(ValidationError),
            "One or more validation errors occurred");

        public static List<ErrorCodeDescription> DefaultErrorCodesList => new()
        {
            SystemError,
            ValidationError
        };
    }
}
