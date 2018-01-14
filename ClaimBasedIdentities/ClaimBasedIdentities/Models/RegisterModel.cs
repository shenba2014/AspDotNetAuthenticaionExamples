using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClaimBasedIdentities.Models
{
	public class RegisterModel
	{
		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string RePassword { get; set; }

		public string Organization { get; set; }
	}
}