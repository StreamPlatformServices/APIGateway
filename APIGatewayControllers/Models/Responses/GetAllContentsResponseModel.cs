using APIGatewayControllers.Models.Base;

namespace APIGatewayControllers.Models.Responses
{
    public class GetAllContentsResponseModel : ContentModel
    {
        public Guid Uuid { set; get; }
        public string ImageUrl { set; get; }
    }
}
