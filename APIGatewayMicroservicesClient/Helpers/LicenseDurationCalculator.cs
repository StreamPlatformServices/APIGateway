using APIGatewayEntities.Entities;
using LicenseProxyAPI.DataMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseProxyAPI.Helpers
{
    public class LicenseDurationCalculator : ILicenseDurationCalculator
    {
        public int CalculateTimeToExpirationInHourse(DateTime licenseActivationTime, LicenseDuration? licenseDuration)
        {
            var elapsedDuration = DateTime.UtcNow - licenseActivationTime;

            var elapsedDurationInHours = (int)elapsedDuration.TotalHours; //TODO: Round??

            return licenseDuration.ToNumberOfHours() - elapsedDurationInHours;
        }
    }
}
