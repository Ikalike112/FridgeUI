using System;

namespace FridgeUI.Models.Auth
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string JwtToken { get; set; }

    }
}
