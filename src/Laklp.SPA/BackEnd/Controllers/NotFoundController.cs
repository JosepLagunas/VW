using Laklp.Security;
using Microsoft.AspNetCore.Mvc;

namespace Laklp.Controllers
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