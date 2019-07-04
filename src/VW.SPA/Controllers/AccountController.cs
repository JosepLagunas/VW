using System.Threading.Tasks;
   using Microsoft.AspNetCore.Authentication;
   using Microsoft.AspNetCore.Authentication.Cookies;
   using Microsoft.AspNetCore.Authorization;
   using Microsoft.AspNetCore.Builder;
   using Microsoft.AspNetCore.Mvc;
   using VWP.Controllers;
   
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

       [Route("login/success")]
       [Authorize]
       public void LoginSuccess()
       {
           Response.Redirect("/home");
       }
   }