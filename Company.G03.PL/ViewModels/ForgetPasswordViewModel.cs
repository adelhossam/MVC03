using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is Required!")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
