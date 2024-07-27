using APIGatewayEntities.Entities;
using LicenseProxyAPI.DataMappers;

namespace LicenseProxyAPI.Helpers
{
    public class LicenseTimeCalculator : ILicenseTimeCalculator
    {
        public int CalculateTimeToExpirationInHourse(DateTime licenseActivationTime, LicenseDuration? licenseDuration)
        {
            var elapsedDuration = DateTime.UtcNow - licenseActivationTime;

            var elapsedDurationInHours = (int)elapsedDuration.TotalHours; //TODO: Round??

            return licenseDuration.ToNumberOfHours() - elapsedDurationInHours;
        }
    }
}
