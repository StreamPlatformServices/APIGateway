namespace APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels
{
    public class ComponentSettings
    {
        public LicenseServiceClientSettings AuthorizationServiceAPI { get; set; }
        public StreamServiceApiSettings StreamServiceAPI { get; set; }
        public LicenseServiceClientSettings LicenseProxyApiSettings { get; set; }
    }
}
