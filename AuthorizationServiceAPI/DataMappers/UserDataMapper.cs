using AuthorizationServiceAPI.Models;
using APIGatewayRouting.Data;
using System.Runtime.CompilerServices;
using AuthorizationServiceAPI.Models.Requests;
using AuthorizationServiceAPI.Models.Responses;

namespace AuthorizationServiceAPI.DataMappers
{
    public static class UserDataMapper
    {
        //TODO: Use dotnet mapper library??
        public static UserRequestDto ToUserRequestDto(this User model)
        {
            if (model is EndUser endUser)
            {
                return new EndUserRequestDto
                {
                    UserName = endUser.UserName,
                    Email = endUser.Email,
                    Password = endUser.Password
                };
            }

            if (model is ContentCreatorUser contentCreatorUser)
            {
                return new ContentCreatorRequestDto
                {
                    UserName = contentCreatorUser.UserName,
                    Email = contentCreatorUser.Email,
                    Password = contentCreatorUser.Password,
                    PhoneNumber = contentCreatorUser.PhoneNumber,
                    NIP = contentCreatorUser.NIP
                };
            }

            return new UserRequestDto
            {
                Email = model.Email,
                Password = model.Password
            };
        }

        public static IEnumerable<User> ToUsers(this IEnumerable<UserResponseDto> models)
        {
            var users = new List<User>();
            foreach (var model in models)
            {
                if (model.Role == "EndUser")
                {
                    users.Add(model.ToEndUser());
                }

                if (model.Role == "ContentCreator")
                {
                    users.Add(model.ToContentCreatorUser());
                }

                if (model.Role == "Admin")
                {
                    users.Add(model.ToAdminUser());
                }
            }
            
            return users;
        }

        public static User ToUser(this UserResponseDto model)
        {
            if (model.Role == "EndUser")
            {
                return model.ToEndUser();
            }

            if (model.Role == "ContentCreator")
            {
                return model.ToContentCreatorUser();
            }

            if (model.Role == "Admin")
            {
                return model.ToAdminUser();
            }

            //TODO: Log error??
            return null;
            
        }
        public static AdminUser ToAdminUser(this UserResponseDto model)
        {
            return new AdminUser
            {
                //Uuid = model.ID, //TODO: Get id from authorization service.. Is it needed???
                UserName = model.UserName,
                Email = model.Email,
                UserLevel = model.Role.ToUserLevel(), //TODO: Validate??? 
                IsActive = model.IsActive
            };
        }
        public static EndUser ToEndUser(this UserResponseDto model)
        {
            return new EndUser
            {
                //Uuid = model.ID, //TODO: Get id from authorization service.. Is it needed???
                UserName = model.UserName,
                Email = model.Email,
                UserLevel = model.Role.ToUserLevel(), //TODO: Validate??? 
                IsActive = model.IsActive
            };
        }

        public static ContentCreatorUser ToContentCreatorUser(this UserResponseDto model)
        {
            return new ContentCreatorUser
            {
                //Uuid = model.ID,
                UserName = model.UserName,
                Email = model.Email,
                UserLevel = model.Role.ToUserLevel(), //TODO: Validate??? 
                IsActive = model.IsActive,
                PhoneNumber = model.PhoneNumber,
                NIP = model.NIP
            };
        }

        public static UserLevel ToUserLevel(this string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return UserLevel.Unknown;
            }

            switch (role)
            {
                case "EndUser": return UserLevel.EndUser;
                    
                case "ContentCreator": return UserLevel.ContentCreator;

                case "Admin": return UserLevel.Administrator;
                    
                default: return UserLevel.Unknown;
            }
        }
        

        
    }

}
