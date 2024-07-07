
using APIGatewayControllers.Models.Responses.Comment;

namespace APIGatewayControllers.Models.Responses.Content
{
    public class GetContentResponseModel
    {
        public string Title { set; get; }
        public int Duration { set; get; }
        public string Description { set; get; }
        public IEnumerable<ContentCommentResponseModel> ContentComments { set; get; }
        public IEnumerable<LicenseRulesModel> LicenseRules { set; get; }
        public string ImageUrl { set; get; }

    }
}
