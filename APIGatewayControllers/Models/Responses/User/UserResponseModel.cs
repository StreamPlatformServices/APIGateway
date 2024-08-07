﻿using APIGatewayEntities.Entities;

namespace APIGatewayControllers.Models.Responses.User
{
    public class UserResponseModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserLevel UserLevel { get; set; }
        public bool IsActive { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NIP { get; set; }
    }
}
