using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models
{
    public class LicenseResponseModel
    {
        public LicenseRulesModel LicenseRulesModel { set; get; }
        public LicenseStatus LicenseStatus { set; get; }
        public int TimeToExpirationInHours { set; get; }
    }
}
