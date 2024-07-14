using APIGatewayEntities.Entities;
using ContentMetadataServiceMock.Persistance.Data;

namespace AuthorizationServiceAPI.DataMappers
{
    public static class ContentDataMapper
    {
        public static ContentData ToContentData(this Content model)
        {
            return new ContentData
            {
                ContentId = model.Uuid,
                Title = model.Title,
                Description = model.Description,
                ContentStatus = model.ContentStatus,
                ImageStatus = model.ImageStatus,
                Comments = model.ContentComments?.Select(c => c.ToContentCommentData()).ToList() ?? new List<ContentCommentData>(),
                LicenseRules = model.LicenseRules?.Select(c => c.ToLicenseRulesData()).ToList() ?? new List<LicenseRulesData>(),
                OwnerId = model.OwnerId,
            };
        }

        public static Content ToContent(this ContentData data)
        {
            return new Content
            {
                Uuid = data.ContentId,
                Title = data.Title,
                Description = data.Description,
                ContentStatus = data.ContentStatus,
                ImageStatus = data.ImageStatus,
                ContentComments = data.Comments?.Select(c => c.ToContentComment()).ToList() ?? new List<ContentComment>(),
                LicenseRules = data.LicenseRules?.Select(c => c.ToLicenseRules()).ToList() ?? new List<LicenseRules>(), //TODO: It is needed
            };
        }

        public static IEnumerable<Content> ToContents(this IEnumerable<ContentData> data)
        {
            var contents = new List<Content>();
            
            foreach (var item in data) 
            {
                contents.Add(item.ToContent());
            }

            return contents;
        }
    }

}
