namespace AuthorizationServiceAPI.Models
{
    public class UserWithRolesDto : UserDto
    {
        public List<string> Roles { get; set; }
    }
}
