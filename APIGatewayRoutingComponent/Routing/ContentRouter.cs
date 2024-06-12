using APIGatewayRouting.Data;
using APIGatewayRouting.Routing.Interfaces;

namespace APIGatewayRouting.Routing
{
    public class ContentRouter : IContentRouter
    {
        async Task<IEnumerable<Content>> IContentRouter.GetAllContentsAsync(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IContentRouter.EditContentAsync(Guid contentId, Content content)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IContentRouter.UploadContentAsync(Content content, ContentLicense license)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IContentRouter.DeleteContentAsync(Guid contentId)
        {
            throw new NotImplementedException();
        }

        public Task<Content> GetContentByNameAsync(string contentName)
        {
            throw new NotImplementedException();
        }
    }
}