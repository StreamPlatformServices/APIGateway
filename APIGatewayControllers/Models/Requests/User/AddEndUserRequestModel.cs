using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.User
{
    public class AddEndUserRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { set; get; }
    }
}
