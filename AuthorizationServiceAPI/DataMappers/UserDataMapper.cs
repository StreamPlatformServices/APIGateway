using AuthorizationServiceAPI.Models;
using APIGatewayRouting.Data;

namespace AuthorizationServiceAPI.DataMappers
{
    public static class UserDataMapper
    {
        public static EndUser ToEndUser(this UserWithRolesDto model)
        {
            return new EndUser
            {
                Uuid = model.ID,
                Name = model.Name,
                Login = "",
                Password = "", //TODO: Remove
                Mail = model.Email,
                UserLevel = model.Roles.ToUserLevel(), //TODO: Validate??? 
            };
        }

        public static UserLevel ToUserLevel(this IEnumerable<string> roles)
        {
            var role = roles.FirstOrDefault();
            if (string.IsNullOrEmpty(role))
            {
                return UserLevel.Unknown;
            }

            switch (role)
            {
                case "EndUser": return UserLevel.EndUser;
                    
                case "ContentCreator": return UserLevel.ContentCreator;

                case "Administrator": return UserLevel.Administrator;
                    
                default: return UserLevel.Unknown;
            }
        }

        //public static EndUserModel ToEndUserModel(this EndUser entity)
        //{
        //    return new EndUserModel
        //    {
        //        Name = entity.Name,
        //        Login = entity.Login,
        //        Password = entity.Password,
        //        Mail = entity.Mail,
        //        UserLevel = entity.UserLevel
        //    };
        //}

        public static ContentCreatorUser ToContentCreatorUser(this UserWithRolesDto model)
        {
            return new ContentCreatorUser
            {
                Uuid = model.ID,
                Name = model.Name,
                Login = "",
                Password = "", //TODO: Remove
                Mail = model.Email,
                UserLevel = model.Roles.ToUserLevel(), //TODO: Validate??? 
                NIP = ""
            };
        }

        //public static ContentCreatorUserModel ToContentCreatorUserModel(this ContentCreatorUser entity)
        //{
        //    return new ContentCreatorUserModel
        //    {
        //        Name = entity.Name,
        //        Login = entity.Login,
        //        Password = entity.Password,
        //        Mail = entity.Mail,
        //        UserLevel = entity.UserLevel,
        //        NIP = entity.NIP
        //    };
        //}
    }

}
