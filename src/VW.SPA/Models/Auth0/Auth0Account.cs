using System.Collections.Generic;

namespace VWP.Models.Auth0
{
    public class Auth0Account
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string Provider { get; set; }

        public string UserId { get; set; }

        public List<string> Roles { get; set; }
        public string AccessToken { get; internal set; }
    }
}