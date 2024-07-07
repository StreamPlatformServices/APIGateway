using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.User
{
    public class UpdateContentCreatorRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }

        [Required]
        public string UserName { set; get; }

        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string NIP { get; set; }

    }
}
