using System;

namespace VWP.Models
{
    public class UserTokens
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime RefreshToken { get; set; }
    }
}