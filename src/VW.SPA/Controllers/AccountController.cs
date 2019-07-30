using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VWP.Controllers;
using VWP.Models;

[Route("[controller]")]
public class AccountController : Controller
{
    [Route("login")]
    public async Task Login(string returnUrl = "/account/login/success")
    {
        await HttpContext.ChallengeAsync("Auth0",
            new AuthenticationProperties() {RedirectUri = returnUrl});
    }

    [Authorize]
    [Route("logout")]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
        {
            // Indicate here where Auth0 should redirect the user after a logout.
            // Note that the resulting absolute Uri must be whitelisted in the
            // **Allowed Logout URLs** settings for the app.
            RedirectUri = Url.Content($"/account/login")
        });
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [Route("logged")]
    public bool IsLoggedIn()
    {
        var isLogged = User.TryGetAccount(HttpContext, out _);

        return isLogged;
    }

    [Route("tokens")]
    [Authorize]
    public async Task<UserTokens> GetCognitoTokens()
    {
        return new UserTokens()
        {
            IdToken = await User.GetId_TokenAsync(HttpContext),
            AccessToken = await User.GetAccess_TokenAsync(HttpContext),
            RefreshToken = await User.GetTokenExpirationAsync(HttpContext)
        };
    }

    [Route("login/success")]
    [Authorize]
    public async Task LoginSuccess()
    {
        Response.Redirect("/home");
    }
}