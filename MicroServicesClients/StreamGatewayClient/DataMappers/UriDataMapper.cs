using StreamGatewayControllers.Models;

namespace StreamGatewayAPI.DataMappers
{
    public static class UriDataMapper
    {
        public static UriData ToUriData(this GetUriResponseModel model)
        {
            return new UriData
            {
                Url = model.Url,
                LifeTime = model.LifeTime,
            };
        }

        public static GetUriResponseModel ToGetUriResponseModel(this UriData entity)
        {
            return new GetUriResponseModel
            {
                Url = entity.Url,
                LifeTime = entity.LifeTime,
            };
        }
        
    }

}
