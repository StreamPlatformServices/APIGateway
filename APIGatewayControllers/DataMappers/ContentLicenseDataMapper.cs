using APIGatewayControllers.Models;
using APIGatewayEntities.Entities;
using System.ComponentModel;

namespace APIGatewayControllers.DataMappers
{
    public static class LicenseDataMapper
    {
        public static ContentLicense ToContentLicense(this LicenseRequestModel model)
        {
            return new ContentLicense
            {
                Uuid = Guid.NewGuid(),
                //UserId = model.UserId, //TODO: will be provided from token (Create license decorator)
                ContentId = model.ContentId,
                LicenseRules = model.LicenseRulesModel.ToLicenseRules()
            };
        }

        public static LicenseResponseModel ToContentLicenseModel(this ContentLicense entity)
        {
            return new LicenseResponseModel
            {
                LicenseRulesModel = entity.LicenseRules.ToLicenseRulesModel(),
                LicenseStatus = entity.LicenseStatus,
                TimeToExpirationInHours = entity.TimeToExpirationInHours
            };
        }
    }

}
