using APIGatewayRouting.Data;
using APIGatewayRouting.Helpers.Interfaces;
using APIGatewayRouting.IntegrationContracts;

namespace APIGatewayRouting.Helpers
{
    internal class ContentFasade : IContentFasade
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
        async Task<bool> IContentFasade.DeleteContentAsync(Guid contentId)
        {
            return await _contentMetadataContract.DeleteContentMetadataAsync(contentId);
        }

        async Task<bool> IContentFasade.EditContentAsync(Guid contentId, Content content)
        {
            return await _contentMetadataContract.EditContentMetadataAsync(contentId, content);
        }

        async Task<IEnumerable<Content>> IContentFasade.GetAllContentsAsync(int limit, int offset)
        {
            return await _contentMetadataContract.GetAllContentsAsync(limit, offset);
        }

        async Task<Content> IContentFasade.GetContentByIdAsync(Guid contentId)
        {

            var content = await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);
            //TODO: Get user by id  or maybe change column in comments database from user id to author name (and maybe the content fasade can be removed)
            //_userContract.GetUserByNameAsync();
            return content;
        }

        async Task<bool> IContentFasade.UploadContentAsync(Content content)
        {
            return await _contentMetadataContract.AddContentMetadataAsync(content);
        }
    }
}
