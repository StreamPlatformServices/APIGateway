using APIGatewayEntities.Entities;

namespace APIGatewayEntities.IntegrationContracts
{
    public interface ILicenseContract
    {
        //TODO: Add LicenseHandler to StreamUriRouter?? Verify license while geting uri or while getting stream???
        Task<ContentLicense> AssignLicenseAsync(Guid userId, Guid contentId);
        Task<bool> IssueLicenseAsync(ContentLicense license);
        Task<bool> VerifyLicenseAsync(Guid userId, Guid contentId);
        Task<bool> DeleteContentMetadataAsync(Guid licenseId);
    }

}
