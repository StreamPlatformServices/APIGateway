using APIGatewayEntities.Entities;

namespace APIGatewayEntities.IntegrationContracts
{
    public interface IStreamUriContract
    {
        Task<string> GetStreamUriAsync(Guid contentId);
        Task<string> GetUploadUriAsync(Guid contentId);
    }

}
