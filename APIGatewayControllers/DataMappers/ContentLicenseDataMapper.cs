using APIGatewayControllers.Models;
using APIGatewayEntities.Entities;

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
                LicenseRules = entity.LicenseRules.ToLicenseRulesModel(),
                LicenseStatus = entity.LicenseStatus,
                TimeToExpirationInHours = entity.TimeToExpirationInHours
            };
        }
    }

}
