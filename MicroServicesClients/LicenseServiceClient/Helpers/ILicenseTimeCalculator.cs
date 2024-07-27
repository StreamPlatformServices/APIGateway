using APIGatewayEntities.Entities;

namespace LicenseProxyAPI.Helpers
{
    public interface ILicenseTimeCalculator
    { 
        int CalculateTimeToExpirationInHourse(DateTime licenseActivationTime, LicenseDuration? licenseDuration);
    }
}
