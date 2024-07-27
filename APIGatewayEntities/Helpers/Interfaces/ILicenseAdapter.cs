using APIGatewayEntities.Entities;

namespace APIGatewayEntities.Helpers.Interfaces
{
    public interface ILicenseAdapter
    {
        Task ExtendLicenseAsync(ContentLicense license, string token);
        Task IssueLicenseAsync(ContentLicense license, string token);
        Task<ContentLicense> GetLicenseAsync(Guid contentId, string token);
    }
}
