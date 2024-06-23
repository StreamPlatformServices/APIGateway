using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models
{
    public class LicenseRulesModel
    {
        public int Prize { set; get; }
        public LicenseType Type { set; get; }
        public LicenseDuration Duration { set; get; }
    }
}