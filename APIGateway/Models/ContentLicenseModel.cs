using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models
{

    public class ContentLicenseModel
    {
        public Guid UserId { set; get; }
        public Guid ContentId { set; get; }
        public LicenseRulesModel LicenseRulesModel { set; get; }
        public LicenseStatus LicenseStatus { set; get; }
        public DateTime ActivationTime { set; get; }
    }
}