using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Base
{
    public class UserModel //TODO: Move to request //change name Request
    {
        [Required]
        public string Email { set; get; }
        [Required]
        public string Password { get; set; }
    }
}
