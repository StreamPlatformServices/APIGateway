namespace APIGatewayControllers.Models.Responses.User
{
    public class UsersResponseModel
    {
        public IEnumerable<UserResponseModel> Users { get; set; }
        public int Count { get; set; }
    }
}
