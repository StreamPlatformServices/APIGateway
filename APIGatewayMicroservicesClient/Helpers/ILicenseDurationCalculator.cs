using APIGatewayEntities.Entities;

namespace LicenseProxyAPI.Helpers
{
    public interface ILicenseDurationCalculator
    {
        int CalculateTimeToExpirationInHourse(DateTime licenseActivationTime, LicenseDuration? licenseDuration);
    }
}
