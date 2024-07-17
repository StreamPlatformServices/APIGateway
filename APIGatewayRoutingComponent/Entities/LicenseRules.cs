using System.Net;
using System.Text.Json.Serialization;

namespace APIGatewayEntities.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LicenseType
    { 
        Unknown,
        Buy,
        Rent
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LicenseDuration
    {
        Unknown,
        OneDay,
        TwoDays,
        ThreeDays,
        Week,
        Month
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LicenseStatus
    {
        Unknown,
        Active,
        Expired
    }

    //TODO:
    //    Czas trwania licencji(np. 24 godziny, 30 dni)
    //    Rodzaj licencji(np.wypożyczenie, zakup)
    //    Regiony, w których licencja jest ważna
    //    Ograniczenia odtwarzania(np.liczba urządzeń, na których można odtworzyć film)

    public class LicenseRules
    {
        public Guid Uuid { set; get; } //TODO is Id needed in entity ?
        public int Price { set; get; }
        public LicenseType Type { set; get; }
        public LicenseDuration? Duration { set; get; }
    }
}