using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models
{
    public class LicenseRequestModel
    {
        public Guid ContentId { set; get; }
        public LicenseRulesModel LicenseRulesModel { set; get; }
    }
}
