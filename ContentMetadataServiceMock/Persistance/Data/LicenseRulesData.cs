using APIGatewayEntities.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentMetadataServiceMock.Persistance.Data
{
    public class LicenseRulesData
    {
        [Key]
        public Guid Uuid { set; get; }
        public int Price { set; get; }
        public LicenseType Type { set; get; }
        public LicenseDuration Duration { set; get; }

        [ForeignKey("content")]
        public Guid ContentId { set; get; }
        public ContentData Content { set; get; }
    }
}
