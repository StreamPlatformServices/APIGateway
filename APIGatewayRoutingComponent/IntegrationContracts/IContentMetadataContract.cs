using APIGatewayEntities.Entities;

namespace APIGatewayEntities.IntegrationContracts
{
    public interface IContentMetadataContract
    {
        Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset);
        Task<Content> GetContentMetadataByIdAsync(Guid contentId);
        Task<IEnumerable<Content>> GetContentMetadataByOwnerIdAsync(Guid contentId);
        Task EditContentMetadataAsync(Guid contentId, Content content);
        Task<Guid> AddContentMetadataAsync(Content content);
        Task DeleteContentMetadataAsync(Guid contentId);
    }
}
