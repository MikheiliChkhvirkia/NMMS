using FluentValidation.Results;
using NMMS.Common.Tools.Extensions;

namespace NMMS.Common.Exceptions
{
    public class AppValidationException : AppException
    {
        public IDictionary<string, string[]> Failures { get; }
        public AppValidationException() : base("AppValidationException", "Validation error(s) occurred")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public AppValidationException(string propertyName, string errorMessage) : this()
        {
            Failures.Add(propertyName, new string[] { errorMessage });
        }

        public AppValidationException(List<ValidationFailure> occurredFailures) : this()
        {
            IEnumerable<string> propertyNames = occurredFailures
                .Select(failure => failure.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                string[] propertyFailures = occurredFailures
                    .Where(failure => failure.PropertyName == propertyName)
                    .Select(failure => failure.ErrorMessage)
                    .Distinct()
                    .ToArray();

                Failures.Add(propertyName.ToCamelCase(), propertyFailures);
            }
        }
    }
}
