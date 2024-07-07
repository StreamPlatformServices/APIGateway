using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;

namespace APIGatewayEntities.Helpers
{
    public class ContentCommentDecorator : IContentCommentContract
    {
        private readonly IContentCommentContract _contentCommentsContract;
        private readonly IUserContract _userContract;

        public ContentCommentDecorator(
            IContentCommentContract contentCommentsContract,
            IUserContract userContract)
        {
            _contentCommentsContract = contentCommentsContract;
            _userContract = userContract;
        }

        public async Task AddCommentAsync(ContentComment comment, string token)
        {
            var user = await _userContract.GetUserAsync(token);

            comment.UserName = user.UserName;

            await _contentCommentsContract.AddCommentAsync(comment, token);
        }
    }
}
