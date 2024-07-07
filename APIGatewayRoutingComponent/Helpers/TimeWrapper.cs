using APIGatewayEntities.Helpers.Interfaces;

namespace APIGatewayEntities.Helpers
{
    internal class TimeWrapper : ITimeWrapper
    {
        DateTime ITimeWrapper.GetCurrentTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
