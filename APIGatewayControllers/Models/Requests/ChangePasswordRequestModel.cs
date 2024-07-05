using APIGatewayControllers.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests
{
    public class ChangePasswordRequestModel
    {
        [Required]
        public string OldPassword { set; get; }
        [Required]
        public string NewPassword { get; set; }
    }
}
