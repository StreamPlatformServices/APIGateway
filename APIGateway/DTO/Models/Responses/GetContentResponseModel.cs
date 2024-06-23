
using System.ComponentModel.DataAnnotations;
using APIGatewayControllers.DTO.Models.Base;

namespace APIGatewayControllers.DTO.Models.Responses
{
    public class GetContentResponseModel : ContentModel
    {
        [Required]
        public string Description { set; get; }
        public IEnumerable<ContentCommentModel> ContentComments { set; get; } 
        public IEnumerable<LicenseRulesModel> LicenseRules { set; get; }
        public string ImageUrl { set; get; }

    }
}
