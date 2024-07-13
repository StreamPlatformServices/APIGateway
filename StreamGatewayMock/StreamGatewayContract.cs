using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels.MockSettings;
using APIGatewayEntities.IntegrationContracts;
using Microsoft.Extensions.Options;
using Nethereum.Web3;

namespace StreamGatewayMock
{
    public class StreamGatewayContract : IStreamUriContract
    {
        private readonly StreamGatewayMockSettings _options;
        public StreamGatewayContract(IOptions<StreamGatewayMockSettings> options) 
        {
            _options = options.Value;
        }  
        public Task<string> GetStreamUriAsync(Guid contentId)
        {
            return Task.FromResult(_options.MockedStreamUrl);
        }

        public async Task<string> GetUploadUriAsync(Guid contentId)
        {
            // Twój Infura Project ID
            string infuraProjectId = "4ecdb1024ecc43ff82b3a8e42fd8c121";

            // Połączenie z Ethereum przez Infura
            string url = $"https://mainnet.infura.io/v3/{infuraProjectId}";
            var web3 = new Web3(url);

            // Sprawdzenie połączenia
            var blockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            Console.WriteLine($"Połączono z Ethereum. Numer ostatniego bloku: {blockNumber.Value}");

            return "";
            //throw new NotImplementedException();
        }
    }
}
