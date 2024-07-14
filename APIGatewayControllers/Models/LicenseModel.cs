using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models
{
    public class LicenseModel
    {
        public Guid UserId { set; get; }
        public Guid ContentId { set; get; }
        public LicenseRulesModel LicenseRulesModel { set; get; }
        public LicenseStatus LicenseStatus { set; get; }
        public DateTime ActivationTime { set; get; } //TODO: It will be better to calculate expiration time. Maybe in Hours
    }
}
