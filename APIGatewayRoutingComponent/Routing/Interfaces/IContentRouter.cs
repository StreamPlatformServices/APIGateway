using APIGatewayRouting.Data;

namespace APIGatewayRouting.Routing.Interfaces
{
    public interface IContentRouter
    {
        Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset);
        Task<Content> GetContentByIdAsync(Guid contentId);
        Task<bool> EditContentAsync(Guid contentId, Content content);
        Task<bool> UploadContentAsync(Content content);
        Task<bool> DeleteContentAsync(Guid contentId);
    }
}