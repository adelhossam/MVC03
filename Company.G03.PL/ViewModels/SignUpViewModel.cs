using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage = "UserName is Required!")]
        public string UserName { get; set; }

		[Required(ErrorMessage = "FirstName is Required!")]
        public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName is Required!")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is Required!")]
		[DataType(DataType.EmailAddress , ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required!")]
		[DataType(DataType.Password)]
		[MinLength(5,ErrorMessage ="Minmum Length is 5")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirmed Password is Required!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage = "The Confirmed Password Dosn't match")]
		public string ConfirmedPassword { get; set; }

		[Required(ErrorMessage = "IsAgree is Required!")]
		public bool IsAgree { get; set; }
    }
}
