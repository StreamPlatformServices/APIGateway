
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.Content
{
    public class UploadContentRequestModel
    {
        [Required]
        public string Title { set; get; }
        [Required]
        public string Description { set; get; }
        [Required]
        public IEnumerable<LicenseRulesModel> LicenseRules { set; get; }
    }
}
