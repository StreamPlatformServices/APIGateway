using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;

namespace APIGatewayMicroservicesClient
{
    public class LicenseProxyAPI : ILicenseContract
    {
        public Task<ContentLicense> AssignLicenseAsync(Guid userId, Guid contentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContentMetadataAsync(Guid licenseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IssueLicenseAsync(ContentLicense license)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyLicenseAsync(Guid userId, Guid contentId)
        {
            throw new NotImplementedException();
        }
    }
}
