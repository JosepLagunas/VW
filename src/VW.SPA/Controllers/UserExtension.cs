using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using VWP.Models.Auth0;

namespace VWP.Controllers
{
    public static class UserExtensions
    {
        public static bool TryGetAccount(this ClaimsPrincipal user, HttpContext httpContext,
            out Auth0Account auth0Account)
        {
            auth0Account = default;

            if (!user.Identity.IsAuthenticated)
                return false;

            auth0Account = new Auth0Account
            {
                Id = GetValueFromClaims(user.Claims, ClaimTypes.NameIdentifier),
                UserId = GetValueFromClaims(user.Claims, ClaimTypes.Email),
                Name = GetValueFromClaims(user.Claims, "name"),
                PictureUrl = GetValueFromClaims(user.Claims, "picture"),
                AccessToken = GetValueFromClaims(user.Claims, "access_token")
            };

            var identities = GetValueFromClaims(user.Claims, "identities");

            if (!string.IsNullOrEmpty(identities))
            {
                var auth0Identities =
                    JsonConvert.DeserializeObject<IList<Auth0Identities>>(identities);

                auth0Account.Provider = auth0Identities.ElementAt(0).Provider;
                auth0Account.UserId = auth0Identities.ElementAt(0).UserId;
            }

            var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            auth0Account.Roles = GetValueFromAppMetadata<List<string>>(user.Claims, "roles", host);

            return true;
        }

        public static string GetUsername(this ClaimsPrincipal user, HttpContext httpContext)
        {
            var isLogged = user.TryGetAccount(httpContext, out var auth0Account);

            return auth0Account.UserId;
        }

        private static string GetValueFromClaims(IEnumerable<Claim> claims, string key)
        {
            var claim = claims.FirstOrDefault(x => x.Type.Equals(key));

            return (claim != null) ? claim.Value : "";
        }

        private static string GetRawValueFromAppMetadata(IEnumerable<Claim> claims, string key,
            string domainUrl)
        {
            var appMetadataClaim =
                claims.FirstOrDefault(x => x.Type.Equals($"{domainUrl}/app_metadata"));

            if (appMetadataClaim == null) return null;

            string value = null;

            var appMetadata = JsonConvert.DeserializeObject<dynamic>(appMetadataClaim.Value);

            var metadataValue = appMetadata[key];

            if (metadataValue != null)
            {
                value = metadataValue.ToString();
            }

            return value;
        }

        private static string GetValueFromAppMetadata(IEnumerable<Claim> claims, string key,
            string domainUrl)
        {
            return GetRawValueFromAppMetadata(claims, key, domainUrl);
        }

        private static T GetValueFromAppMetadata<T>(IEnumerable<Claim> claims, string key,
            string domainUrl) where T : new()
        {
            var rawValue = GetRawValueFromAppMetadata(claims, key, domainUrl);

            return string.IsNullOrEmpty(rawValue)
                ? new T()
                : JsonConvert.DeserializeObject<T>(rawValue);
        }
    }
}