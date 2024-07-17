using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models.Responses.Content
{
    public class ContentCreatorContentsResponseModel
    {
        public Guid Uuid { get; set; }    
        public string Title { set; get; }
        public int Duration { set; get; }
        public string Description { set; get; }
        public UploadState ImageStatus { set; get; } = UploadState.NoFile;
        public UploadState ContentStatus { set; get; } = UploadState.NoFile;
        public IEnumerable<LicenseRulesModel> LicenseRules { set; get; }

    }
}
