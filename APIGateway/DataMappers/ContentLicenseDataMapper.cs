using APIGatewayController.Models;
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
                Uuid = Guid.Empty,
                Description = model.Description,
                UploadTime = model.UploadTime,
                ExpirationTime = model.ExpirationTime,
                LicenseType = model.LicenseType,
                LicenseStatus = model.LicenseStatus
            };
        }

        public static ContentLicenseModel ToContentLicenseModel(this ContentLicense entity)
        {
            return new ContentLicenseModel
            {
                Description = entity.Description,
                UploadTime = entity.UploadTime,
                ExpirationTime = entity.ExpirationTime,
                LicenseType = entity.LicenseType,
                LicenseStatus = entity.LicenseStatus
            };
        }
    }

}
