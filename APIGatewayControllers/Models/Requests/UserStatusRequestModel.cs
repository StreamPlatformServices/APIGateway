using APIGatewayControllers.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests
{
    public class UserStatusRequestModel
    {
        [Required]
        public bool Status { set; get; }
        
    }
}
