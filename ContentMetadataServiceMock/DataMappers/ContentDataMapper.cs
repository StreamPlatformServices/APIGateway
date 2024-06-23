using APIGatewayRouting.Data;
using ContentMetadataServiceMock.Persistance.Data;
using System.ComponentModel.DataAnnotations;

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
                Comments = model.ContentComments?.Select(c => c.ToContentCommentData()).ToList() ?? new List<ContentCommentData>(),
                LicenseRules = model.LicenseRules?.Select(c => c.ToLicenseRulesData()).ToList() ?? new List<LicenseRulesData>(),
            };
        }

        public static Content ToContent(this ContentData data)
        {
            return new Content
            {
                Uuid = data.ContentId,
                Title = data.Title,
                Description = data.Description,
                ContentComments = data.Comments?.Select(c => c.ToContentComment()).ToList() ?? new List<ContentComment>(),
                LicenseRules = data.LicenseRules?.Select(c => c.ToLicenseRules()).ToList() ?? new List<LicenseRules>(), //TODO: It is needed
            };
        }
    }

}
