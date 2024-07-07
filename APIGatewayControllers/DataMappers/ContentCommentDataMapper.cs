using APIGatewayControllers.Models.Requests.Comment;
using APIGatewayControllers.Models.Responses.Comment;
using APIGatewayEntities.Entities;

namespace APIGatewayControllers.DataMappers
{
    public static class ContentCommentDataMapper
    {
        public static ContentComment ToContentComment(this ContentCommentRequestModel model)
        {
            return new ContentComment
            {
                Uuid = Guid.NewGuid(),
                ContentId = model.ContentId,
                Body = model.Body,
                CreationTime = DateTime.UtcNow
            };
        }

        public static ContentCommentResponseModel ToContentCommentModel(this ContentComment entity)
        {
            return new ContentCommentResponseModel
            {
                Uuid = entity.Uuid,
                Body = entity.Body,
                CreationTime = entity.CreationTime,
                UserName = entity.UserName
            };
        }
    }


}
