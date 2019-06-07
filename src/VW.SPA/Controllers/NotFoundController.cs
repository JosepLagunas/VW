using Microsoft.AspNetCore.Mvc;

namespace VWP.Controllers
{
    public class NotFoundController : Controller
    {
        // GET
        public void HandleNotFound()
        {
            var isUserLogged = User.TryGetAccount(HttpContext, out _);

            Response.Redirect(isUserLogged ? "/home" : "/account/login?returnUrl=/home");
        }
    }
}