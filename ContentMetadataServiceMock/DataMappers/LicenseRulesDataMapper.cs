using APIGatewayEntities.Entities;
using ContentMetadataServiceMock.Persistance.Data;

namespace AuthorizationServiceAPI.DataMappers
{
    public static class LicenseRulesDataMapper
    {
        public static LicenseRulesData ToLicenseRulesData(this LicenseRules model)
        {
            return new LicenseRulesData
            {
                Uuid = model.Uuid,
                Price = model.Price,
                Type = model.Type,
                Duration = model.Duration //TODO: is FK id needed???
            };
        }

        public static LicenseRules ToLicenseRules(this LicenseRulesData data)
        {
            return new LicenseRules
            {
                Uuid = data.Uuid,
                Price = data.Price,
                Type = data.Type,
                Duration = data.Duration
            };
        }
    }

}
