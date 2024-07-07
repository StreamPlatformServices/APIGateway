using APIGatewayEntities.Entities;
using ContentMetadataServiceMock.Persistance.Data;

namespace AuthorizationServiceAPI.DataMappers
{
    public static class ContentCommentDataMapper
    {
        

        public static ContentCommentData ToContentCommentData(this ContentComment entity)
        {

            return new ContentCommentData
            {
                ContentCommentId = entity.Uuid,
                ContentId = entity.ContentId,
                Body = entity.Body,
                UserName = entity.UserName,
                CreationTime = entity.CreationTime
            };
        }

        public static ContentComment ToContentComment(this ContentCommentData data)
        {

            return new ContentComment
            {
                Uuid = data.ContentCommentId,
                Body = data.Body,
                UserName = data.UserName,
                CreationTime = data.CreationTime
            };
        }



    }

}
