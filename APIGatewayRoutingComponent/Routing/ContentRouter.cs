using APIGatewayRouting.Data;
using APIGatewayRouting.Helpers;
using APIGatewayRouting.Helpers.Interfaces;
using APIGatewayRouting.IntegrationContracts;
using APIGatewayRouting.Routing.Interfaces;
using System.Collections.Generic;

namespace APIGatewayRouting.Routing
{
    
    public class ContentRouter : IContentRouter
    {
        //private readonly IContentFasade _contentFasade;

        private readonly IContentMetadataContract _contentMetadataContract;

        public ContentRouter(IContentMetadataContract contentMetadataContract)
        {
            _contentMetadataContract = contentMetadataContract;
        }

        //public ContentRouter(IContentFasade contentFasade)
        //{
        //    _contentFasade = contentFasade;
        //}

        async Task<IEnumerable<Content>> IContentRouter.GetAllContentsAsync(int limit, int offset)
        {
            return await _contentMetadataContract.GetAllContentsAsync(limit, offset);
        }

        async Task<bool> IContentRouter.EditContentAsync(Guid contentId, Content content)
        {
            return await _contentMetadataContract.EditContentMetadataAsync(contentId, content);
        }

        async Task<bool> IContentRouter.UploadContentAsync(Content content)
        {
            return await _contentMetadataContract.AddContentMetadataAsync(content);
        }

        async Task<bool> IContentRouter.DeleteContentAsync(Guid contentId)
        {
            return await _contentMetadataContract.DeleteContentMetadataAsync(contentId);
        }

        async Task<Content> IContentRouter.GetContentByIdAsync(Guid contentId)
        {
            return await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);
        }

        //async Task<IEnumerable<Content>> IContentRouter.GetAllContentsAsync(int limit, int offset)
        //{
        //    return await _contentFasade.GetAllContentsAsync(limit, offset);
        //}

        //async Task<bool> IContentRouter.EditContentAsync(Guid contentId, Content content)
        //{
        //    return await _contentFasade.EditContentAsync(contentId, content);
        //}

        //async Task<bool> IContentRouter.UploadContentAsync(Content content)
        //{
        //    return await _contentFasade.UploadContentAsync(content); 
        //}

        //async Task<bool> IContentRouter.DeleteContentAsync(Guid contentId)
        //{
        //    return await _contentFasade.DeleteContentAsync(contentId);
        //}

        //async Task<Content> IContentRouter.GetContentByIdAsync(Guid contentId)
        //{
        //    return await _contentFasade.GetContentByIdAsync(contentId);
        //}

    }
}