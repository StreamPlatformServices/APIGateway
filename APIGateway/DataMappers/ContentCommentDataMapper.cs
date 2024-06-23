using APIGatewayControllers.DTO.Models;
using APIGatewayRouting.Data;

namespace APIGatewayControllers.DataMappers
{
    public static class ContentCommentDataMapper
    {
        public static ContentComment ToContentComment(this ContentCommentModel model)
        {
            return new ContentComment
            {
                Uuid = model.Uuid,
                Body = model.Body,
                CreationTime = model.CreationTime,
                UserName = model.AuthorName
            };
        }

        public static ContentCommentModel ToContentCommentModel(this ContentComment entity)
        {
            return new ContentCommentModel
            {
                Uuid = entity.Uuid,
                Body = entity.Body,
                CreationTime = entity.CreationTime,
                AuthorName = entity.UserName
            };
        }
    }


}
