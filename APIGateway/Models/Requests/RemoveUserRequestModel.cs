using APIGatewayControllers.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests
{
    public class RemoveUserRequestModel
    {
        [Required]
        public string Password { get; set; }
    }
}
