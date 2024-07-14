using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;
using LicenseProxyAPI.DataMappers;
using LicenseProxyAPI.Helpers;

namespace LicenseProxyAPI
{
    public class LicenseContract : ILicenseContract
    {
        private readonly ILicenseDurationCalculator _durationCalculator;
        public LicenseContract(ILicenseDurationCalculator licenseDurationCalculator) 
        {
            _durationCalculator = licenseDurationCalculator;
        }
        public Task DeleteLicenseAsync(Guid licenseId)
        {
            throw new NotImplementedException();
        }

        public Task ExtendLicenseAsync(ContentLicense license, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ContentLicense> GetLicenseAsync(Guid contentId, string token)
        {
            //TODO: NOW!!!
            //1. Configure HttpClient!
            //2. Integrate some DRM!!!
            var licenseDto = new ContentLicenseDto
            {
                LicenseRules = new LicenseRules
                {
                    Duration = LicenseDuration.Week
                },

                ActivationTime = DateTime.UtcNow.AddDays(-1)
            };

            var license = licenseDto.ToContentLicense();

            license.TimeToExpirationInHours = _durationCalculator
                .CalculateTimeToExpirationInHourse(license.ActivationTime, license.LicenseRules.Duration);

            return license;
        }

        public Task IssueLicenseAsync(ContentLicense license, string token)
        {
            throw new NotImplementedException();
        }
    }
}
