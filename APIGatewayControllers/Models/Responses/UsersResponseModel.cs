namespace APIGatewayControllers.Models.Responses
{
    public class UsersResponseModel
    {
        public IEnumerable<UserResponseModel> Users { get; set; }
        public int Count { get; set; }
    }
}
