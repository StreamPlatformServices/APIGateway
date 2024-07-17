
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.Content
{
    public class UploadContentRequestModel
    {
        [Required]
        public string Title { set; get; }
        [Required]
        public string Description { set; get; }

        [Required(ErrorMessage = "The LicenseRules field is required.")]
        [MinLength(1, ErrorMessage = "The array must contain at least one element.")]
        public IEnumerable<LicenseRulesModel> LicenseRules { set; get; }

        [Required]
        public Guid VideoFileId { set; get; }

        [Required]
        public Guid ImageFileId { set; get; }
    }
}
