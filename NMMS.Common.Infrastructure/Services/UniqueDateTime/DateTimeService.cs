using NMMS.Common.Application.Interfaces.UniqueDateTime;

namespace NMMS.Common.Infrastructure.Services.UniqueDateTime
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
