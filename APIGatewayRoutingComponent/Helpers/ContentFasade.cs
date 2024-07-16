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
        private readonly IStreamUriContract _streamUriContract; 

        public ContentFasade(
            IContentMetadataContract contentMetadataContract,
            ILicenseContract licenseContract,
            IUserContract userContract,
            IStreamUriContract streamUriContract)
        {
            _contentMetadataContract = contentMetadataContract;
            _licenseContract = licenseContract;
            _userContract = userContract;
            _streamUriContract = streamUriContract;
        }

        public async Task<IEnumerable<Content>> GetContentByUserTokenAsync(string token)
        {
            var user = await _userContract.GetUserAsync(token);

            return await _contentMetadataContract.GetContentMetadataByOwnerIdAsync(user.Uuid);
        }

        async Task<bool> IContentFasade.DeleteContentAsync(Guid contentId)
        {
            var content = await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);
            if (content.ImageStatus != UploadState.NoFile || content.ContentStatus != UploadState.NoFile)
            {
                return false;
            }

            await _contentMetadataContract.DeleteContentMetadataAsync(contentId);
            return true;
        }

        async Task IContentFasade.EditContentAsync(Guid contentId, Content content)
        {
            //TODO: Validate the owner id in auth service???
            await _contentMetadataContract.EditContentMetadataAsync(contentId, content);
        }

        async Task<IEnumerable<Content>> IContentFasade.GetAllContentsAsync(int limit, int offset)
        {
            var contents = await _contentMetadataContract.GetAllContentsAsync(limit, offset);

            var tasks = new List<Task>();

            foreach (var content in contents) //TODO: Maybe create method which allows to get list of images with one req. Or create some smart caching structure!!!!!!!!!!!!!
            {
                tasks.Add(UpdateContentWithImageUrlAsync(content));
            }

            await Task.WhenAll(tasks);

            return contents;
        }

        private async Task UpdateContentWithImageUrlAsync(Content content)
        {
            var uriData = await _streamUriContract.GetImageStreamUriAsync(content.Uuid);
            content.ImageUrl = uriData.Url;
        }

        async Task<Content> IContentFasade.GetContentByIdAsync(Guid contentId)
        {
            var content =  await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);

            await UpdateContentWithImageUrlAsync(content);

            return content;
        }

        async Task<Guid> IContentFasade.UploadContentAsync(Content content, string token)
        {
            var user = await _userContract.GetUserAsync(token);

            content.OwnerId = user.Uuid;

            return await _contentMetadataContract.AddContentMetadataAsync(content);
        }
    }
}
