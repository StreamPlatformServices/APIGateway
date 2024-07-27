using System.Text.Json.Serialization;

namespace APIGatewayEntities.Entities //TODO: change namespace to Entities
{
    //TODO: Check if it is used in model
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserLevel //TODO: enums in seperate files
    {
        Unknown,
        EndUser,
        ContentCreator,
        Administrator
    }

    //public enum UserStatus //TODO: enums in seperate files
    //{
    //    Unknown,
    //    Active,
    //    Disabled
    //}

    public class User
    {
        public Guid Uuid { set; get; }
        public string UserName { set; get; }
        public string? Password { set; get; }
        public string Email { set; get; }
        public UserLevel UserLevel { set; get; }
        public bool IsActive { set; get; }

        public DateTime CreationTime { set; get; }
        //TODO: public DateTime UpdateTime { set; get; }
    }
}
