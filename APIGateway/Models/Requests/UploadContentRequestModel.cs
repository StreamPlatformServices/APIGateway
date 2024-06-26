
using System.ComponentModel.DataAnnotations;
using APIGatewayControllers.Models;
using APIGatewayControllers.Models.Base;

namespace APIGatewayControllers.Models.Requests
{
    public class UploadContentRequestModel : ContentModel
    {
        [Required]
        public string Description { set; get; }
        [Required]
        public IEnumerable<LicenseRulesModel> LicenseRulesModel { set; get; }
    }
}
