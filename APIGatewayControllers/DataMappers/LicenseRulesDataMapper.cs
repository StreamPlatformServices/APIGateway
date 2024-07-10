using APIGatewayControllers.Models;
using APIGatewayEntities.Entities;
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
                Price = model.Price,
                Type = model.Type,
                Duration = model.Duration
            };
        }

        public static LicenseRulesModel ToLicenseRulesModel(this LicenseRules entity)
        {
            return new LicenseRulesModel
            {
                Price = entity.Price,
                Type = entity.Type,
                Duration = entity.Duration
            };
        }
    }

}
