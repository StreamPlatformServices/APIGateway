using APIGatewayEntities.Entities;

namespace APIGatewayEntities.Helpers.Interfaces
{
    public interface ILicenseChecker
    {
        bool IsActive(ContentLicense license);
    }
}
