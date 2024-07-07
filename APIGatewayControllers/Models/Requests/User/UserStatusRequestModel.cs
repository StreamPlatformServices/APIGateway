using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.User
{
    public class UserStatusRequestModel
    {
        [Required]
        public bool Status { set; get; }

    }
}
