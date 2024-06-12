using APIGatewayRouting.Helpers.Interfaces;

namespace APIGatewayRouting.Helpers
{
    internal class TimeWrapper : ITimeWrapper
    {
        DateTime ITimeWrapper.GetCurrentTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
