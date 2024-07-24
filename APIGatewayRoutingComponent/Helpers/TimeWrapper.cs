using APIGatewayEntities.Helpers.Interfaces;

namespace APIGatewayEntities.Helpers
{
    public class TimeWrapper : ITimeWrapper
    {
        DateTime ITimeWrapper.GetCurrentTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
