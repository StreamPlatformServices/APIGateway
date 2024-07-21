using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models
{
    public class LicenseResponseModel
    {
        public Guid Uuid { set; get; }
        public EncryptionKey KeyData { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
