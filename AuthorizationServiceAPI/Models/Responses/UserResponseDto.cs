﻿namespace AuthorizationServiceAPI.Models.Responses
{
    //TODO: NOW!!! Create models for all methods -> Use draw.io 
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string? PhoneNumber { get; set; }

        public string? NIP { get; set; }
    }
}
