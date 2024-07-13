using APIGatewayEntities.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContentMetadataServiceMock.Persistance.Data
{
    public class ContentData
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ContentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public ICollection<ContentCommentData> Comments { get; set; } 
        public ICollection<LicenseRulesData> LicenseRules { set; get; }
        public Guid OwnerId { get; set; }
        public UploadState ImageStatus { set; get; } = UploadState.InProgress;
        public UploadState ContentStatus { set; get; } = UploadState.InProgress;
    }
}
