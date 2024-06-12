
namespace APIGatewayRouting.Routing.Interfaces
{
    public interface IStreamUriRouter
    {
        Task<string> GetStreamUriAsync(string contentName);
        Task<string> GetUploadUriAsync(string contentName);
    }

}
