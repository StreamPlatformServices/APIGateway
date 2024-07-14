using APIGatewayEntities.Entities;

namespace APIGatewayEntities.Helpers.Interfaces
{
    public interface IContentFasade
    {
        Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset);
        Task<Content> GetContentByIdAsync(Guid contentId);
        Task<IEnumerable<Content>> GetContentByUserTokenAsync(string token);
        Task EditContentAsync(Guid contentId, Content content);
        Task<Guid> UploadContentAsync(Content content, string token);
        Task DeleteContentAsync(Guid contentId);
    }
}
