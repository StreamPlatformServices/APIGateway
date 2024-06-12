
using APIGatewayControllers.Models;
using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayController.Models
{
    public class EndUserModel : UserModel
    {
        public string PhoneNumber { set; get; } //TODO: It is needed? Probably not
    }
}
