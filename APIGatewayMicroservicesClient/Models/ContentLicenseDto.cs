using System.Net;

namespace APIGatewayEntities.Entities
{
    public class ContentLicenseDto
    {
        public Guid Uuid { set; get; }
        public Guid UserId { set; get; }
        public Guid ContentId { set; get; }
        public LicenseRules LicenseRules { set; get; }
        public LicenseStatus LicenseStatus { set; get; }
        public DateTime ActivationTime { set; get; }
    }
}