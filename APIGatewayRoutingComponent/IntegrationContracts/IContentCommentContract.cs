using APIGatewayEntities.Entities;

namespace APIGatewayEntities.IntegrationContracts
{
    public interface IContentCommentContract
    {
        Task AddCommentAsync(ContentComment comment, string token);
    }
}
