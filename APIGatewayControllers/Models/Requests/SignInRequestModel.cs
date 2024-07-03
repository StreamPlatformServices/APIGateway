using APIGatewayControllers.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests
{
    public class SignInRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        public string Password { get; set; }
    }
}
