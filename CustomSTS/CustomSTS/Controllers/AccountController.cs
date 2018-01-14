using CustomSTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomSTS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string returnUrl)
        {
			ViewBag.ReturnUrl = returnUrl;
            return View();
        }

		[HttpPost]
		public ActionResult Login(LoginModel model, string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "please input username and password");
				return View(model);
			}

			if (!model.UserName.Equals("admin") || !model.Password.Equals("password"))
			{
				ModelState.AddModelError("", "invalid username or password");
				return View(model);
			}

			FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

			return Redirect(returnUrl);
		}
    }
}