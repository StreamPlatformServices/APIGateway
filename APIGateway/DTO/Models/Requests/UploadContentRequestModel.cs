
using System.ComponentModel.DataAnnotations;
using APIGatewayControllers.DTO.Models.Base;

namespace APIGatewayControllers.DTO.Models.Requests
{
    public class UploadContentRequestModel : ContentModel
    {
        [Required]
        public string Description { set; get; }
        [Required]
        public IEnumerable<LicenseRulesModel> LicenseRulesModel { set; get; }
    }
}
