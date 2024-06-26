using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Base
{
    public class ContentCreatorUserModel : UserModel
    {
        [Required]
        public string UserName { set; get; }

        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string NIP { get; set; }
    }
}
