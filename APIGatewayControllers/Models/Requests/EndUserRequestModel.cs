using APIGatewayControllers.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models.Requests
{
    public class EndUserRequestModel 
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
