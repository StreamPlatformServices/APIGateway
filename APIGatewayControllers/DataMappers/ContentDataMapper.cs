using APIGatewayControllers.Models;
using APIGatewayControllers.Models.Requests.Content;
using APIGatewayControllers.Models.Responses.Comment;
using APIGatewayControllers.Models.Responses.Content;
using APIGatewayEntities.Entities;

namespace APIGatewayControllers.DataMappers
{
    public static class ContentDataMapper
    {
        public static Content ToContent(this UploadContentRequestModel model)
        {
            return new Content
            {
                Uuid = Guid.NewGuid(),
                Title = model.Title,
                UploadTime = DateTime.UtcNow, //TODO: UT???
                Description = model.Description,
                LicenseRules = model.LicenseRulesModel?.Select(c => c.ToLicenseRules()).ToList() ?? new List<LicenseRules>(),
            };
        }

        public static IEnumerable<GetAllContentsResponseModel> ToGetAllContentsResponseModel(this IEnumerable<Content> entities) 
        {
            var contents = new List<GetAllContentsResponseModel>();
            foreach (var content in entities) 
            {
                contents.Add(new GetAllContentsResponseModel
                {
                    Title = content.Title,
                    Duration = content.Duration,
                    ImageUrl = content.ImageUrl,
                    Uuid = content.Uuid
                });
            }

            return contents;
        }

        public static GetContentResponseModel ToGetContentResponseModel(this Content entity) 
        {
            return new GetContentResponseModel
            {
                Title = entity.Title,
                Description = entity.Description,
                ContentComments = entity.ContentComments?.Select(c => c.ToContentCommentModel()).ToList() ?? new List<ContentCommentResponseModel>(),
                LicenseRules = entity.LicenseRules?.Select(c => c.ToLicenseRulesModel()).ToList() ?? new List<LicenseRulesModel>(),
            };
        }
    }

}
