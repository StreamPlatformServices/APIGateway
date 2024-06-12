using APIGatewayRouting.Data;
using APIGatewayRouting.IntegrationContracts;
using Nethereum.Web3;

namespace StreamGatewayMock
{
    public class StreamGatewayContract : IStreamUriContract
    {
        Task<string> IStreamUriContract.GetStreamUri(Guid contentId)
        {
            return Task.FromResult(@"https://cdn.flowplayer.com/a30bd6bc-f98b-47bc-abf5-97633d4faea0/v-de3f6ca7-2db3-4689-8160-0f574a5996ad.mp4");
        }

        async Task<string> IStreamUriContract.GetUploadUri(Guid contentId)
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
