using APIGatewayControllers.DTO.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models
{
    public class ContentWithLicenseModel
    {
        [Required]
        public UploadContentRequestModel ContentModel { get; set; }

        [Required]
        public LicenseRulesModel LicenseModel { get; set; }
    }
}
