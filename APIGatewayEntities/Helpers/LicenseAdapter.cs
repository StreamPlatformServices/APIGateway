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
        private readonly ILicenseChecker _licenseChecker;

        public LicenseAdapter(
            ILicenseContract licenseContract,
            IUserContract userContract,
            IContentMetadataContract contentMetadataContract,
            ILicenseChecker licenseChecker)
        {
            _licenseContract = licenseContract;
            _userContract = userContract;
            _contentMetadataContract = contentMetadataContract;
            _licenseChecker = licenseChecker;
        }

        public async Task<ContentLicense> GetLicenseAsync(Guid contentId, string token)
        {
            var user = await _userContract.GetUserAsync(token);

            var content  = await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);

            var license = await _licenseContract.GetLicenseAsync(user.Uuid, content.VideoFileId, token);

            if (!_licenseChecker.IsActive(license))
            {
                license.KeyData = null;
            }

            return license;
        }
        
        public async Task IssueLicenseAsync(ContentLicense license, string token)
        {
            var user = await _userContract.GetUserAsync(token);
            license.UserId = user.Uuid;

            var content = await _contentMetadataContract.GetContentMetadataByIdAsync(license.ContentId);
            license.FileId = content.VideoFileId;

            await _licenseContract.IssueLicenseAsync(license, token);
        }

        public async Task ExtendLicenseAsync(ContentLicense license, string token)
        {
            var user = await _userContract.GetUserAsync(token);
            license.UserId = user.Uuid;

            var content = await _contentMetadataContract.GetContentMetadataByIdAsync(license.ContentId);
            license.FileId = content.VideoFileId;

            await _licenseContract.ExtendLicenseAsync(license, token);
        }

        public async Task DeleteLicenseAsync(Guid licenseId)
        {
            await _licenseContract.DeleteLicenseAsync(licenseId);
        }

       
    }
}
