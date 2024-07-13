using APIGatewayEntities.Entities;
using StreamGatewayControllers.Models;

namespace APIGatewayEntities.IntegrationContracts
{
    public interface IStreamUriContract
    {
        Task<UriData> GetVideoStreamUriAsync(Guid contentId);
        Task<UriData> GetImageStreamUriAsync(Guid contentId);
    }

}
