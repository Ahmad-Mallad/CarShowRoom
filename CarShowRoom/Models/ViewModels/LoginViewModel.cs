using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "enter your email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "enter ur password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
