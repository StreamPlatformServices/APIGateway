using APIGatewayControllers.DTO.Models;
using APIGatewayRouting.Data;
using System.ComponentModel;

namespace APIGatewayControllers.DataMappers
{
    public static class LicenseDataMapper
    {
        public static ContentLicense ToContentLicense(this ContentLicenseModel model)
        {
            return new ContentLicense
            {
                Uuid = Guid.NewGuid(),
                UserId = model.UserId,
                ContentId = model.ContentId,
                LicenseRules = model.LicenseRulesModel.ToLicenseRules()
            };
        }

        public static ContentLicenseModel ToContentLicenseModel(this ContentLicense entity)
        {
            return new ContentLicenseModel
            {
                UserId = entity.UserId,
                ContentId = entity.ContentId,
                LicenseRulesModel = entity.LicenseRules.ToLicenseRulesModel()
            };
        }
    }

}
