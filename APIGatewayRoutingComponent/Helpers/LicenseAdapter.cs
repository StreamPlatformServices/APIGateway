using APIGatewayEntities.Entities;
using APIGatewayEntities.Helpers.Interfaces;
using APIGatewayEntities.IntegrationContracts;

namespace APIGatewayEntities.Helpers
{
    public class LicenseAdapter : ILicenseAdapter
    {
        private readonly ILicenseContract _licenseContract;
        private readonly IUserContract _userContract;
        private readonly IContentMetadataContract _contentMetadataContract;

        public LicenseAdapter(
            ILicenseContract licenseContract,
            IUserContract userContract,
            IContentMetadataContract contentMetadataContract)
        {
            _licenseContract = licenseContract;
            _userContract = userContract;
            _contentMetadataContract = contentMetadataContract;
        }

        public async Task<ContentLicense> GetLicenseAsync(Guid contentId, string token)
        {
            var user = await _userContract.GetUserAsync(token);

            var content  = await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);

            return await _licenseContract.GetLicenseAsync(user.Uuid, content.VideoFileId, token);
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
