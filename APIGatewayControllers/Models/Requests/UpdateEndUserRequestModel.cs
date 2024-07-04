using APIGatewayControllers.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models.Requests
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
