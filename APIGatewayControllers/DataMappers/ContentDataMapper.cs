using APIGatewayControllers.Models;
using APIGatewayControllers.Models.Requests;
using APIGatewayControllers.Models.Responses;
using APIGatewayRouting.Data;

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

        //public static UploadContentRequestModel ToUploadContentRequestModel(this Content entity) 
        //{
        //    return new UploadContentRequestModel
        //    {
        //        Title = entity.Title,
        //        Description = entity.Description,
        //        LicenseRulesModel = entity.LicenseRules?.Select(c => c.ToLicenseRulesModel()).ToList() ?? new List<LicenseRulesModel>()
        //    };
        //}

        //TODO: NOW. Finish all mappers!!!!

        public static GetAllContentsResponseModel ToGetAllContentsResponseModel(this Content entity) 
        {
            return new GetAllContentsResponseModel
            {
                Title = entity.Title,
                Duration = entity.Duration,
                ImageUrl = entity.ImageUrl,
                Uuid = entity.Uuid
            };
        }

        public static GetContentResponseModel ToGetContentResponseModel(this Content entity) 
        {
            return new GetContentResponseModel
            {
                Title = entity.Title,
                Description = entity.Description,
                ContentComments = entity.ContentComments?.Select(c => c.ToContentCommentModel()).ToList() ?? new List<ContentCommentModel>(),
                LicenseRules = entity.LicenseRules?.Select(c => c.ToLicenseRulesModel()).ToList() ?? new List<LicenseRulesModel>(),
            };
        }
    }

}
