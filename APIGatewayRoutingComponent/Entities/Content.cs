using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIGatewayEntities.Entities
{
    public class Content
    {
        public Guid Uuid { set; get; } = Guid.Empty; //TODO: It is empty by default???
        public string Title { set; get; }
        public DateTime UploadTime { set; get; }
        public int Duration { set; get; }
        public string ImageUrl { set; get; }
        public string Description { set; get; }
        public IEnumerable<LicenseRules> LicenseRules { set; get; }
        public IEnumerable<ContentComment> ContentComments { set; get; }
        public Guid OwnerId { set; get; }
        public Guid VideoFileId { set; get; }
        public Guid ImageFileId { set; get; }
    }
}
