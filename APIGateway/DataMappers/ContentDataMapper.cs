using APIGatewayController.Models;
using APIGatewayRouting.Data;

namespace APIGatewayControllers.DataMappers
{
    public static class ContentDataMapper
    {
        public static Content ToContent(this ContentModel model)
        {
            return new Content
            {
                Uuid = Guid.Empty,
                Name = model.Name,
                UploadTime = model.UploadTime,
                Description = model.Description,
                ContentComments = model.ContentComments?.Select(c => c.ToContentComment()).ToList() ?? new List<ContentComment>() 
            };
        }

        public static ContentModel ToContentModel(this Content entity)
        {
            return new ContentModel
            {
                Name = entity.Name,
                UploadTime = entity.UploadTime,
                Description = entity.Description,
                ContentComments = entity.ContentComments?.Select(c => c.ToContentCommentModel()).ToList() ?? new List<ContentCommentModel>()
            };
        }
    }

}
