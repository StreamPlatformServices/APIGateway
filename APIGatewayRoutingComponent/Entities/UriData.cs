using System.Text.Json.Serialization;

namespace StreamGatewayControllers.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UrlLifeTime
    {
        Unknown,
        Infinit,
        Once
        //TimeLimited
    }
    public class UriData
    {
        public string Url { get; set; }
        public UrlLifeTime LifeTime { get; set; }
    }
}
