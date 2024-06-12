using APIGatewayRouting.Helpers.Interfaces;
using APIGatewayRouting.IntegrationContracts;
using APIGatewayRouting.Routing.Interfaces;

namespace APIGatewayRouting.Routing
{
    public class StreamUriRouter : IStreamUriRouter
    {
        private readonly IStreamUriContract _streamUriContract;

        public StreamUriRouter(
            IStreamUriContract streamUriContract)
        {
            _streamUriContract = streamUriContract;
        }

        async Task<string> IStreamUriRouter.GetStreamUriAsync(string contentName)
        {
            throw new NotImplementedException();
        }

        async Task<string> IStreamUriRouter.GetUploadUriAsync(string contentName)
        {
            return await _streamUriContract.GetUploadUri(Guid.Empty);
        }
    }

}
