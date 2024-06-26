using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Base
{
    public class EndUserModel : UserModel
    {
        [Required]
        public string UserName { set; get; }
    }
}
