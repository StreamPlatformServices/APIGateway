using APIGatewayRouting.Data;

namespace APIGatewayRouting.IntegrationContracts
{
    public interface IStreamUriContract
    {
        Task<string> GetStreamUri(Guid contentId);
        Task<string> GetUploadUri(Guid contentId);
    }

}
