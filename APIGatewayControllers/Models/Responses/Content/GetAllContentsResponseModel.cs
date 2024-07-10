using APIGatewayControllers.Models.Responses.User;

namespace APIGatewayControllers.Models.Responses.Content
{
    public class GetAllContentsResponseModel
    {
        public IEnumerable<ContentsResponseModel> Contents { get; set; }
        public int Count { get; set; }
    }
}
