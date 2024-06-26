
using System.ComponentModel.DataAnnotations;
using APIGatewayControllers.Models;
using APIGatewayControllers.Models.Base;

namespace APIGatewayControllers.Models.Responses
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
