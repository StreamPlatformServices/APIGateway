namespace APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels
{
    internal class RateLimitingSettings
    {
        public int RequestLimitPerIp { get; set; }
        public int TimeSpanSecondsPerIp { get; set; }
        public int GlobalRequestLimit { get; set; }
        public int GlobalTimeSpanSeconds { get; set; }
        
    }
}
