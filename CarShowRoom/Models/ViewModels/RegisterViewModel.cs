using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "enter your email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "enter confirm email")]
		[EmailAddress]
		[Compare("Email", ErrorMessage = "email and confirm not match")]
		public string ConfirmEmail { get; set; }

		[Required(ErrorMessage = "enter ur password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "enter confirm password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "password and confirm not match")]
		public string ConfirmPassword { get; set; }

		public string Mobile { get; set; }

	}
}
