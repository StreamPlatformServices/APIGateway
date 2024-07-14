using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models
{
    public class LicenseResponseModel
    {
        public LicenseRulesModel LicenseRules { set; get; }
        public LicenseStatus LicenseStatus { set; get; }
        public int TimeToExpirationInHours { set; get; }
    }
}
