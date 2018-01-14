using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
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
				//if (FormsAuthentication.Authenticate(model.UserName, model.Password))
				if (Membership.ValidateUser(model.UserName, model.Password))
				{
					var user = Membership.GetUser(model.UserName);

					signIn(user);

					return Redirect(FormsAuthentication.DefaultUrl);
				}
			}
			ModelState.AddModelError("", "Invalid user name");

			return View(model);
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterModel model)
		{
			FederatedAuthentication.SessionAuthenticationModule.SignOut();

			if (ModelState.IsValid)
			{
				if (model.Password != model.RePassword)
				{
					ModelState.AddModelError("RepeatPasswordError", "Could not verify password...");
					return View(model);
				}

				var user = Membership.CreateUser(model.UserName, model.Password, model.Email);
				var profile = ProfileBase.Create(user.UserName, true);
				profile.SetPropertyValue("Organization", model.Organization);
				profile.Save();

				signIn(user);

				return Redirect(FormsAuthentication.DefaultUrl);
			}
			return View(model);
		}


		public ActionResult SignOut()
	    {
		    FederatedAuthentication.SessionAuthenticationModule.SignOut();
		    return RedirectToAction("Login");
	    }

		private void signIn(MembershipUser user)
		{
			var claims = new[]
					{
						new Claim(ClaimTypes.NameIdentifier, user.Email),
						new Claim(ClaimTypes.Name, user.UserName)
					};

			var profile = ProfileBase.Create(user.UserName, true);
			var claimList = new List<Claim>(claims);
			foreach (SettingsProperty property in ProfileBase.Properties)
			{
				claimList.Add(new Claim(property.Attributes["CustomProviderData"].ToString()
					, profile[property.Name].ToString()));
			}

			var identity = new ClaimsIdentity(claimList, "Forms");
			var principal = new ClaimsPrincipal(identity);

			var token = new SessionSecurityToken(principal);

			FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(token);
		}
    }
}