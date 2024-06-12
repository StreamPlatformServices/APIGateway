using APIGatewayRouting.Data;
using APIGatewayRouting.Routing.Interfaces;

namespace APIGatewayRouting.Routing
{
    public class ContentCommentRouter : IContentCommentRouter
    {
        //async Task<IEnumerable<ContentComment>> IContentCommentRouter.GetCommentsByContentNameAsync(string contentName)
        //{
        //    throw new NotImplementedException();
        //}
        async Task<bool> IContentCommentRouter.RemoveCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IContentCommentRouter.EditCommentAsync(Guid commentId, ContentComment comment)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IContentCommentRouter.AddCommentAsync(ContentComment comment)
        {
            throw new NotImplementedException();
        }        
    }
}