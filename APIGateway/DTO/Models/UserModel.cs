using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models
{
    public class UserModel //TODO: UserRequest and Response different classes
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; } //TODO: Remove
        [Required]
        public string Password { get; set; }
        [Required]
        public string Mail { set; get; } //TODO: Email
        public UserLevel UserLevel { set; get; }
        public DateTime CreationTime { set; get; } //TODO: It is needed??
    }
}
