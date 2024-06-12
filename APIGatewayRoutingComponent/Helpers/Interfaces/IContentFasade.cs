using APIGatewayRouting.Data;

namespace APIGatewayRouting.Helpers.Interfaces
{
    internal interface IContentFasade
    {
        Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset);
        Task<Content> GetContentByNameAsync(string contentName);
        Task<bool> EditContentAsync(Guid contentId, Content content);
        Task<bool> UploadContentAsync(Content content, ContentLicense license);
        Task<bool> DeleteContentAsync(Guid contentId);
    }
}
