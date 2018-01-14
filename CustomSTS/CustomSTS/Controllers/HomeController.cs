using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomSTS.Controllers
{
    public class HomeController : Controller
    {
		public const string Action = "wa";
		public const string SignIn = "wsignin1.0";

		// GET: Home
		public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
			{
				var action = Request.QueryString[Action];

				if (action == SignIn)
				{
					//var formData = 
					return new ContentResult() { ContentType = "text/html" };
				}
			}
			return View();
        }
    }
}