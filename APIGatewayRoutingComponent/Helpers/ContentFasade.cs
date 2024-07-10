using APIGatewayEntities.Entities;
using APIGatewayEntities.Helpers.Interfaces;
using APIGatewayEntities.IntegrationContracts;

namespace APIGatewayEntities.Helpers
{
    public class ContentFasade : IContentFasade //TODO: change to decorator?
    {
        private readonly IContentMetadataContract _contentMetadataContract;
        private readonly ILicenseContract _licenseContract; //TODO: probably not needed ??
        private readonly IUserContract _userContract; 

        public ContentFasade(
            IContentMetadataContract contentMetadataContract,
            ILicenseContract licenseContract,
            IUserContract userContract)
        {
            _contentMetadataContract = contentMetadataContract;
            _licenseContract = licenseContract;
            _userContract = userContract;
        }

        public async Task<IEnumerable<Content>> GetContentByUserTokenAsync(string token)
        {
            var user = await _userContract.GetUserAsync(token);

            return await _contentMetadataContract.GetContentMetadataByOwnerIdAsync(user.Uuid);
        }

        async Task IContentFasade.DeleteContentAsync(Guid contentId)
        {
            await _contentMetadataContract.DeleteContentMetadataAsync(contentId);
        }

        async Task IContentFasade.EditContentAsync(Guid contentId, Content content)
        {
            //TODO: Validate the owner id in auth service???
            await _contentMetadataContract.EditContentMetadataAsync(contentId, content);
        }

        async Task<IEnumerable<Content>> IContentFasade.GetAllContentsAsync(int limit, int offset)
        {
            return await _contentMetadataContract.GetAllContentsAsync(limit, offset);
        }

        async Task<Content> IContentFasade.GetContentByIdAsync(Guid contentId)
        {
            return await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);
        }

        async Task IContentFasade.UploadContentAsync(Content content, string token)
        {
            var user = await _userContract.GetUserAsync(token);

            content.OwnerId = user.Uuid;

            await _contentMetadataContract.AddContentMetadataAsync(content);
        }
    }
}
