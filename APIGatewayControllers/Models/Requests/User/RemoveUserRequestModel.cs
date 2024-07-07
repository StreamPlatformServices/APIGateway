using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.User
{
    public class RemoveUserRequestModel
    {
        [Required]
        public string Password { get; set; }
    }
}
