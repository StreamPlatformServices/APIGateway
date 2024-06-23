using APIGatewayRouting.Data;

namespace APIGatewayRouting.Helpers.Interfaces
{
    public interface IContentFasade
    {
        Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset);
        Task<Content> GetContentByIdAsync(Guid contentId);
        Task<bool> EditContentAsync(Guid contentId, Content content);
        Task<bool> UploadContentAsync(Content content);
        Task<bool> DeleteContentAsync(Guid contentId);
    }
}
