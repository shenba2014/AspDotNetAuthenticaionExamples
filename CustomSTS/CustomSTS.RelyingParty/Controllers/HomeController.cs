using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomSTS.RelyingParty.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {

		// GET: Home
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult SiginOut()
		{
			FederatedAuthentication.SessionAuthenticationModule.SignOut();
			return RedirectToAction("Index");
		}
    }
}