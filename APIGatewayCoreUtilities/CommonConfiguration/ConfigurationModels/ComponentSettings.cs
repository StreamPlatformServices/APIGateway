namespace APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels
{
    public class ComponentSettings
    {
        public LicenseProxyApiSettings AuthorizationServiceAPI { get; set; }
        public StreamServiceApiSettings StreamServiceAPI { get; set; }
        public LicenseProxyApiSettings LicenseProxyApiSettings { get; set; }
    }
}
