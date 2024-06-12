using APIGatewayController.Models;
using APIGatewayRouting.Data;

namespace APIGatewayControllers.DataMappers
{
    public static class UserDataMapper
    {
        public static EndUser ToEndUser(this EndUserModel model)
        {
            return new EndUser
            {
                Uuid = Guid.Empty,
                Name = model.Name,
                Login = model.Login,
                Password = model.Password,
                Mail = model.Mail,
                UserLevel = model.UserLevel
            };
        }

        public static EndUserModel ToEndUserModel(this EndUser entity)
        {
            return new EndUserModel
            {
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Mail = entity.Mail,
                UserLevel = entity.UserLevel
            };
        }

        public static ContentCreatorUser ToContentCreatorUser(this ContentCreatorUserModel model)
        {
            return new ContentCreatorUser
            {
                Uuid = Guid.Empty,
                Name = model.Name,
                Login = model.Login,
                Password = model.Password,
                Mail = model.Mail,
                UserLevel = model.UserLevel,
                NIP = model.NIP
            };
        }

        public static ContentCreatorUserModel ToContentCreatorUserModel(this ContentCreatorUser entity)
        {
            return new ContentCreatorUserModel
            {
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Mail = entity.Mail,
                UserLevel = entity.UserLevel,
                NIP = entity.NIP
            };
        }
    }

}
