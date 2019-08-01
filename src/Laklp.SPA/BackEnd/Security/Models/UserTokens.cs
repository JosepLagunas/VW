using System;

namespace Laklp.Security.Models
{
    public class UserTokens
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime RefreshToken { get; set; }
    }
}