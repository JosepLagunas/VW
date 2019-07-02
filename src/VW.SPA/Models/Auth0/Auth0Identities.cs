using Newtonsoft.Json;

namespace VWP.Models.Auth0
{
    public class Auth0Identities
    {
        public string Provider { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        public string Connection { get; set; }

        public bool IsSocial { get; set; }
    }
}