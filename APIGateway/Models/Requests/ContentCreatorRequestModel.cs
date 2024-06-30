using APIGatewayControllers.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models.Requests
{
    public class ContentCreatorRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { set; get; }

        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string NIP { get; set; }

    }
}
