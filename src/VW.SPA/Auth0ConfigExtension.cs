using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace VWP
{
    public static class Auth0ConfigExtension
    {
        public static IServiceCollection AddAuth0Authentication(this IServiceCollection services,
            string domain, string clientId, string clientSecret)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add authentication services
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect("Auth0", options =>
                {
                    // Set the authority to your Auth0 domain
                    options.Authority = $"https://{domain}";

                    // Configure the Auth0 Client ID and Client Secret
                    options.ClientId = clientId;
                    options.ClientSecret = clientSecret;

                    // Set response type to code
                    options.ResponseType = "code";

                    // Configure the scope
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("email");
                    options.Scope.Add("profile");

                    // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
                    // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                    options.CallbackPath = new PathString("/account/login/callback");

                    // Configure the Claims Issuer to be Auth0
                    options.ClaimsIssuer = "Auth0";

                    options.Events = new OpenIdConnectEvents
                    {
                        // handle the logout redirection
                        OnRedirectToIdentityProviderForSignOut = (context) =>
                        {
                            var logoutUri =
                                $"https://{domain}/v2/logout?client_id={clientId}";

                            var postLogoutUri = context.Properties.RedirectUri;
                            if (!string.IsNullOrEmpty(postLogoutUri))
                            {
                                if (postLogoutUri.StartsWith("/"))
                                {
                                    // transform to absolute
                                    var request = context.Request;
                                    postLogoutUri =
                                        request.Scheme + "://" + request.Host + request.PathBase +
                                        postLogoutUri;
                                }

                                logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
                            }

                            context.Response.Redirect(logoutUri);
                            context.HandleResponse();

                            return Task.CompletedTask;
                        },
                        OnTicketReceived = context =>
                        {
                            // Get the ClaimsIdentity
                            if (!(context.Principal.Identity is ClaimsIdentity identity))
                                return Task.CompletedTask;

                            // Check if token names are stored in Properties
                            if (!context.Properties.Items.ContainsKey(".TokenNames"))
                                return Task.CompletedTask;

                            // Token names a semicolon separated
                            var tokenNames = context.Properties.Items[".TokenNames"]
                                .Split(';');

                            // Add each token value as Claim
                            foreach (var tokenName in tokenNames)
                            {
                                // Tokens are stored in a Dictionary with the Key ".Token.<token name>"
                                var tokenValue =
                                    context.Properties.Items[$".Token.{tokenName}"];

                                identity.AddClaim(new Claim(tokenName, tokenValue));
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}