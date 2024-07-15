using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models
{
    public class LicenseRulesModel
    {
        public int Price { set; get; }
        public LicenseType Type { set; get; }
        public LicenseDuration? Duration { set; get; }
    }
}