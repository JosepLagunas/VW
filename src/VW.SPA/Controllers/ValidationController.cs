using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

namespace VWP.Controllers
{
    public class ValidationController : Controller
    {
        public void HandleValidation(string returnUrl = "/")
        {
            if (Request.Path.HasValue && Request.Path != "")
            {
                returnUrl = Request.Path;
            }

            var isUserLogged = User.TryGetAccount(HttpContext, out _);

            if (isUserLogged)
            {
                Response.Redirect(returnUrl);
            }

            Response.Redirect($"/account/login?returnUrl={returnUrl}");
        }
    }
}