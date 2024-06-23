using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models
{
    public class EndUserModel : UserModel
    {
        public string PhoneNumber { set; get; } //TODO: It is needed? Probably not
    }
}
