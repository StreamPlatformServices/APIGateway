
using APIGatewayControllers.Models;
using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayController.Models
{
    public class ContentCreatorUserModel : UserModel
    {
        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string NIP { get; set; }
    }
}
