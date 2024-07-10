using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models.Responses.Content
{
    public class ContentCreatorContentsResponseModel
    {
        public Guid Uuid { get; set; }    
        public string Title { set; get; }
        public int Duration { set; get; }
        public string Description { set; get; }
        public UploadStatus ImageStatus { set; get; }
        public UploadStatus ContentStatus { set; get; }
        public IEnumerable<LicenseRulesModel> LicenseRules { set; get; }

    }
}
