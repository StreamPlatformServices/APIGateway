using APIGatewayControllers.Models;
using APIGatewayEntities.Entities;
using System.ComponentModel;

namespace APIGatewayControllers.DataMappers
{
    public static class LicenseDataMapper
    {
        public static ContentLicense ToContentLicense(this LicenseModel model)
        {
            return new ContentLicense
            {
                Uuid = Guid.NewGuid(),
                UserId = model.UserId,
                ContentId = model.ContentId,
                LicenseRules = model.LicenseRulesModel.ToLicenseRules()
            };
        }

        public static LicenseModel ToContentLicenseModel(this ContentLicense entity)
        {
            return new LicenseModel
            {
                UserId = entity.UserId,
                ContentId = entity.ContentId,
                LicenseRulesModel = entity.LicenseRules.ToLicenseRulesModel()
            };
        }
    }

}
