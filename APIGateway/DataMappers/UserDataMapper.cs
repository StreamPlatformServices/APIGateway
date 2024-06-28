using APIGatewayControllers.DTO.Models.Requests;
using APIGatewayControllers.Models.Base;
using APIGatewayControllers.Models.Responses;
using APIGatewayRouting.Data;

namespace APIGatewayControllers.DataMappers
{
    public static class UserDataMapper
    {
        public static UsersResponseModel ToUsersResponseModel(this IEnumerable<User> entities)
        {
            var users = new List<UserResponseModel>(); //TODO: copy or reference???
            foreach (var entity in entities)
            {
                users.Add(entity.ToUserResponseModel());
            }

            return new UsersResponseModel { Users = users};
        }

        public static UserResponseModel ToUserResponseModel(this User entity)
        {
            if (entity is EndUser endUser)
            {
                return new UserResponseModel
                {
                    UserName = endUser.UserName,
                    Email = endUser.Email,
                    UserLevel = UserLevel.EndUser,
                    IsActive = endUser.IsActive,
                };
            }

            if (entity is ContentCreatorUser contentCreator)
            {
                return new UserResponseModel
                {
                    UserName = contentCreator.UserName,
                    Email = contentCreator.Email,
                    UserLevel = UserLevel.ContentCreator,
                    IsActive = contentCreator.IsActive,
                    PhoneNumber = contentCreator.PhoneNumber,
                    NIP = contentCreator.NIP
                };
            }

            if (entity is AdminUser adminUser)
            {
                return new UserResponseModel
                {
                    UserName = adminUser.UserName,
                    Email = adminUser.Email,
                    UserLevel = UserLevel.Administrator,
                    IsActive = adminUser.IsActive,
                };
            }


            //TODO: >>>???
            return null;
        }

        public static EndUser ToEndUser(this EndUserRequestModel model)
        {
            return new EndUser
            {
                Uuid = Guid.Empty,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                UserLevel = UserLevel.EndUser
            };
        }

        public static ContentCreatorUser ToContentCreatorUser(this ContentCreatorRequestModel model)
        {
            return new ContentCreatorUser
            {
                Uuid = Guid.NewGuid(),
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserLevel = UserLevel.ContentCreator,
                NIP = model.NIP
            };
        }
        
    }

}
