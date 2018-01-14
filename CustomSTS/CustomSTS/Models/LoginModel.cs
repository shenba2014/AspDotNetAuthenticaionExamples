using System.ComponentModel.DataAnnotations;

namespace CustomSTS.Models
{
	public class LoginModel
	{
		[Required]
		public string UserName { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}