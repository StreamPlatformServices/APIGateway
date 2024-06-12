namespace APIGatewayRouting.Helpers.Interfaces
{
    //TODO: Maybe classes used in different components should be in Utilities folder???
    public interface ITimeWrapper
    {
        DateTime GetCurrentTimeUtc();
    }
}
