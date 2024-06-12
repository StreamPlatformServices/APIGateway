using APIGatewayRouting.Data;

namespace APIGatewayRouting.Routing.Interfaces
{
    public interface IContentCommentRouter
    {
        //Task<IEnumerable<ContentComment>> GetCommentsByContentNameAsync(string contentName);
        Task<bool> RemoveCommentAsync(Guid commentId);
        Task<bool> EditCommentAsync(Guid commentId, ContentComment comment);
        Task<bool> AddCommentAsync(ContentComment comment);
    }
}
