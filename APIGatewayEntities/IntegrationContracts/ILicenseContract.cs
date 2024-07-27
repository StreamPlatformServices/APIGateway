using APIGatewayEntities.Entities;

namespace APIGatewayEntities.IntegrationContracts
{
    public interface ILicenseContract //TODO: change use cases accordingly
    {
        //TODO: Add LicenseHandler to StreamUriRouter?? Verify license while geting uri or while getting stream???

        Task ExtendLicenseAsync(ContentLicense license, string token);
        Task IssueLicenseAsync(ContentLicense license, string token);
        Task<ContentLicense> GetLicenseAsync(Guid userId, Guid fileId, string token);
        Task DeleteLicenseAsync(Guid licenseId); //TODO: While removing content remove all licenses??
    }

}
