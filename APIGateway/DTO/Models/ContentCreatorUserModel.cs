using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models
{
    public class ContentCreatorUserModel : UserModel
    {
        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string NIP { get; set; }
    }
}
