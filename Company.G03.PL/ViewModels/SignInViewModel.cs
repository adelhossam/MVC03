using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email is Required!")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required!")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Minmum Length is 5")]
		public string Password { get; set; }

		public bool RemeberMe { get; set; }
	}
}
