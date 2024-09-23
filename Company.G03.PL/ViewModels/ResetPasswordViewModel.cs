using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required!")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Minmum Length is 5")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirmed Password is Required!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "The Confirmed Password Dosn't match")]
		public string ConfirmedPassword { get; set; }
	}
}
