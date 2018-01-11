using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClaimBasedIdentities.Models;

namespace ClaimBasedIdentities.Controllers
{
    public class AuthController : Controller
    {
		[HttpGet]
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

		[HttpPost]
	    public ActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				if (FormsAuthentication.Authenticate(model.UserName, model.Password))
				{
					var claims = new[]
					{
						new Claim(ClaimTypes.NameIdentifier, model.UserName),
						new Claim(ClaimTypes.Name, model.UserName)
					};
					var identity = new ClaimsIdentity(claims, "Forms");
					var principal = new ClaimsPrincipal(identity);

					var token = new SessionSecurityToken(principal);

					FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(token);

					return Redirect(FormsAuthentication.DefaultUrl);
				}
			}
			ModelState.AddModelError("", "Invalid user name");

			return View(model);
		}

	    public ActionResult SignOut()
	    {
		    FederatedAuthentication.SessionAuthenticationModule.SignOut();
		    return RedirectToAction("Login");
	    }
    }
}