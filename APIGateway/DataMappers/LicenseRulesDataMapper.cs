using APIGatewayControllers.DTO.Models;
using APIGatewayRouting.Data;
using System.ComponentModel;

namespace APIGatewayControllers.DataMappers
{
    public static class LicenseRulesDataMapper
    {
        public static LicenseRules ToLicenseRules(this LicenseRulesModel model)
        {
            return new LicenseRules
            {
                Uuid = Guid.NewGuid(),
                Prize = model.Prize,
                Type = model.Type,
                Duration = model.Duration
            };
        }

        public static LicenseRulesModel ToLicenseRulesModel(this LicenseRules entity)
        {
            return new LicenseRulesModel
            {
                Prize = entity.Prize,
                Type = entity.Type,
                Duration = entity.Duration
            };
        }
    }

}
