namespace NMMS.Common.Application.Interfaces.UniqueDateTime
{
    //This service is used for set same DateTime value without different milisecconds in every place of project during one HttpRequest
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
