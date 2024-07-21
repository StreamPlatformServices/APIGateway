using APIGatewayControllers.Models;
using APIGatewayEntities.Entities;

namespace APIGatewayControllers.DataMappers
{
    public static class LicenseDataMapper
    {
        private const int HOURS_IN_DAY = 24;
        public static int ToNumberOfHours(this LicenseDuration? licenseDuration) //TODO: Change ToNumberOfDays???
        {
            if (licenseDuration == null)
            {
                return 0;
            }

            if (licenseDuration == LicenseDuration.Unknown)
            {
                return 0;
            }

            switch (licenseDuration)
            {
                case LicenseDuration.OneDay: return HOURS_IN_DAY;

                case LicenseDuration.TwoDays: return HOURS_IN_DAY * 2;
                case LicenseDuration.ThreeDays: return HOURS_IN_DAY * 3;
                case LicenseDuration.Week: return HOURS_IN_DAY * 7;
                case LicenseDuration.Month: return HOURS_IN_DAY * 30; //TODO: Change to thirty days???

                default: return 0;
            }
        }
        public static ContentLicense ToContentLicense(this LicenseRequestModel model)
        {
            return new ContentLicense
            {
                Uuid = Guid.NewGuid(),
                //UserId = model.UserId, //TODO: will be provided from token (Create license decorator)
                //FileId = model.ContentId,
                StartTime = DateTime.UtcNow,
                EndTime = model.LicenseRulesModel.Type == LicenseType.Rent ? 
                    DateTime.UtcNow.AddHours(model.LicenseRulesModel.Duration.ToNumberOfHours()) :
                    DateTime.MaxValue
            };
        }

        public static LicenseResponseModel ToContentLicenseModel(this ContentLicense entity)
        {
            return new LicenseResponseModel
            {
                Uuid = entity.Uuid,
                KeyData = entity.KeyData,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }
    }

}
