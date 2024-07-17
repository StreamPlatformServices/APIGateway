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
                LicenseRules = model.LicenseRules?.Select(c => c.ToLicenseRules()).ToList() ?? new List<LicenseRules>(),
            };
        }

        public static GetAllContentsResponseModel ToGetAllContentsResponseModel(this IEnumerable<Content> entities) 
        {
            var contents = new List<ContentsResponseModel>();
            foreach (var content in entities) 
            {
                contents.Add(new ContentsResponseModel
                {
                    Title = content.Title,
                    Duration = content.Duration,
                    ImageUrl = content.ImageUrl,
                    Uuid = content.Uuid
                });
            }

            return new GetAllContentsResponseModel { Contents = contents, Count = contents.Count };
        }

        public static GetContentResponseModel ToGetContentResponseModel(this Content entity) 
        {
            return new GetContentResponseModel
            {
                Title = entity.Title,
                Duration = entity.Duration,
                Description = entity.Description,
                ContentComments = entity.ContentComments?.Select(c => c.ToContentCommentModel()).ToList() ?? new List<ContentCommentResponseModel>(),
                LicenseRules = entity.LicenseRules?.Select(c => c.ToLicenseRulesModel()).ToList() ?? new List<LicenseRulesModel>(), //TODO: pytanie czy w zgodzie ze standardem nie moznabyłoby zwracac id'kow licencji
                ImageUrl = entity.ImageUrl
            };
        }

        public static ContentCreatorContentsResponseModel ToContentCreatorContentsResponseModel (this Content entity)
        {
            return new ContentCreatorContentsResponseModel
            {
                Uuid = entity.Uuid,
                Title = entity.Title,
                Duration = entity.Duration,
                Description = entity.Description,
                ImageStatus = entity.ImageStatus,
                ContentStatus = entity.ContentStatus,
                LicenseRules = entity.LicenseRules?.Select(c => c.ToLicenseRulesModel()).ToList() ?? new List<LicenseRulesModel>(),
            };
        }

        public static GetContentsByUserResponseModel ToGetContentsByUserResponseModel(this IEnumerable<Content> entities)
        {
            var contents = new List<ContentCreatorContentsResponseModel>();

            foreach (var entity in entities)
            {
                contents.Add(entity.ToContentCreatorContentsResponseModel());
            }

            return new GetContentsByUserResponseModel { ContentCreatorContents = contents, Count = contents.Count };
        }
    }

}
