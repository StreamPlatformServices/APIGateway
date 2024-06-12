using APIGatewayRouting.Data;

namespace APIGatewayRouting.Routing.Interfaces
{
    public interface IContentRouter
    {
        Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset);
        Task<Content> GetContentByNameAsync(string contentName);
        Task<bool> EditContentAsync(Guid contentId, Content content);
        Task<bool> UploadContentAsync(Content content, ContentLicense license);
        Task<bool> DeleteContentAsync(Guid contentId);
    }
}