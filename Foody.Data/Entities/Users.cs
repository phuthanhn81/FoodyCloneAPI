using System;

namespace Foody.Data.Entities
{
    public class Users
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? Activated { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string Email { get; set; }
    }
}
