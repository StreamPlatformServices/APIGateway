using APIGatewayRouting.Data;
using ContentMetadataServiceMock.Persistance.Data;

namespace AuthorizationServiceAPI.DataMappers
{
    public static class ContentCommentDataMapper
    {
        

        public static ContentCommentData ToContentCommentData(this ContentComment model)
        {

            return new ContentCommentData
            {
                ContentCommentId = model.Uuid,
                Body = model.Body,
                UserName = model.UserName,
            };
        }

        public static ContentComment ToContentComment(this ContentCommentData model)
        {

            return new ContentComment
            {
                Uuid = model.ContentCommentId,
                Body = model.Body,
                UserName = model.UserName,
            };
        }



    }

}
