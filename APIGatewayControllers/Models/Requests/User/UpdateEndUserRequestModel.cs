using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.User
{
    public class UpdateEndUserRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }

        [Required]
        public string UserName { set; get; }
    }
}
