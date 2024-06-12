using APIGatewayController.Models;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models
{
    public class ContentWithLicenseModel
    {
        [Required]
        public ContentModel ContentModel { get; set; }

        [Required]
        public ContentLicenseModel LicenseModel { get; set; }
    }
}
