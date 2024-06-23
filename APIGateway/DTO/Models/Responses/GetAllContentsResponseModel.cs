
using APIGatewayControllers.DTO.Models.Base;

namespace APIGatewayControllers.DTO.Models.Responses
{
    public class GetAllContentsResponseModel : ContentModel
    {
        public Guid Uuid { set; get; }
        public string ImageUrl { set; get; }
    }
}
