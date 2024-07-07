using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.User
{
    public class ChangePasswordRequestModel
    {
        [Required]
        public string OldPassword { set; get; }
        [Required]
        public string NewPassword { get; set; }
    }
}
