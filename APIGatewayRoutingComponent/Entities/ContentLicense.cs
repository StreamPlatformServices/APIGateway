using System.Net;

namespace APIGatewayEntities.Entities
{
    public class ContentLicense
    {
        public Guid Uuid { set; get; }
        public Guid UserId { set; get; }
        public Guid ContentId { set; get; }
        public LicenseRules LicenseRules { set; get; }
    }
}