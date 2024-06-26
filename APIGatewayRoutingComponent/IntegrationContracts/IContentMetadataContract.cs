﻿using APIGatewayRouting.Data;

namespace APIGatewayRouting.IntegrationContracts
{
    public interface IContentMetadataContract
    {
        Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset);
        Task<Content> GetContentMetadataByIdAsync(Guid contentId);
        Task<bool> EditContentMetadataAsync(Guid contentId, Content content);
        Task<bool> AddContentMetadataAsync(Content content);
        Task<bool> DeleteContentMetadataAsync(Guid contentId);
    }
}