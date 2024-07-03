using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models
{
    public class LicenseRulesModel
    {
        public int Prize { set; get; }
        public LicenseType Type { set; get; }
        public LicenseDuration Duration { set; get; }
    }
}