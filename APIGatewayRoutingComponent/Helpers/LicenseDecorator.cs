using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;

namespace APIGatewayEntities.Helpers
{
    public class LicenseDecorator : ILicenseContract
    {
        private readonly ILicenseContract _licenseContract;
        private readonly IUserContract _userContract;

        public LicenseDecorator(
            ILicenseContract licenseContract,
            IUserContract userContract)
        {
            _licenseContract = licenseContract;
            _userContract = userContract;
        }

        public async Task<ContentLicense> GetLicenseAsync(Guid contentId, string token)
        {
            return await _licenseContract.GetLicenseAsync(contentId, token);
        }

        public async Task IssueLicenseAsync(ContentLicense license, string token)
        {
            var user = await _userContract.GetUserAsync(token);

            license.UserId = user.Uuid;

            await _licenseContract.IssueLicenseAsync(license, token);
        }

        public async Task ExtendLicenseAsync(ContentLicense license, string token)
        {
            await _licenseContract.ExtendLicenseAsync(license, token);
        }

        public async Task DeleteLicenseAsync(Guid licenseId)
        {
            await _licenseContract.DeleteLicenseAsync(licenseId);
        }
    }
}
