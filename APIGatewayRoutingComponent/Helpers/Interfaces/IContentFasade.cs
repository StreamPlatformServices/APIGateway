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

        /// <summary>
        /// Removing content metadata is only possible, when all files related with content id are already removed.
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns>Returns false when files associated with content still exists</returns>
        Task<bool> DeleteContentAsync(Guid contentId);
    }
}
