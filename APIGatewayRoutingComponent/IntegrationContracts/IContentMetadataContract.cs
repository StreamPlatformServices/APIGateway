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

        //TODO: Temp. Maybe figure out how to make the oparations with files and metadata atomic
        Task UpdateContentImageFileStateAsync(Guid contentId, UploadState uploadState);
        Task UpdateContentVideoFileStateAsync(Guid contentId, UploadState uploadState);
    }
}
