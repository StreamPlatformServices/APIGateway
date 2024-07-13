using StreamGatewayControllers.Models;

namespace APIGatewayControllers.DataMappers
{
    //TOOD: Remove unneeded
    public static class StreamUriDataMapper
    {
        public static UriData ToUriData(this GetUriResponseModel model)
        {
            return new UriData
            {
                Url = model.Url,
                LifeTime = model.LifeTime,
            };
        }

        public static GetUriResponseModel ToGetResponseModel(this UriData entity)
        {
            return new GetUriResponseModel
            {
                Url = entity.Url,
                LifeTime = entity.LifeTime,
            };
        }
    }


}
