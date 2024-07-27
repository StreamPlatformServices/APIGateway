using APIGatewayEntities.Entities;
using APIGatewayEntities.Helpers.Interfaces;

namespace APIGatewayEntities.Helpers
{
    public class LicenseChecker : ILicenseChecker //TODO: UT
    {
        private readonly ITimeWrapper _timeWrapper;
        public LicenseChecker(ITimeWrapper timeWrapper) 
        {
            _timeWrapper = timeWrapper;
        }
        public bool IsActive(ContentLicense license)
        {
            var now = _timeWrapper.GetCurrentTimeUtc();
            return (license.StartTime <= now && now <= license.EndTime);
        }
    }
}
