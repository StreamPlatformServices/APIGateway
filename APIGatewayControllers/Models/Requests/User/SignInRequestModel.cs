using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.User
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
