using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;
using AuthorizationServiceAPI.DataMappers;
using ContentMetadataServiceMock.Persistance;
using Microsoft.Extensions.Logging;

namespace ContentMetadataServiceMock
{
    public class ContentCommentContract : IContentCommentContract
    {
        private readonly ILogger<ContentCommentContract> _logger;
        private readonly ContentMetadataDatabaseContext _context;

        public ContentCommentContract(
            ILogger<ContentCommentContract> logger,
            ContentMetadataDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddCommentAsync(ContentComment comment, string token)
        {
            try
            {
                await _context.ContentComments.AddAsync(comment.ToContentCommentData());
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
