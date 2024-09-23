using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Company.G03.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
		    _signInManager = signInManager;
		}
		[HttpGet]
        public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid) 
			{
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user is null) 
				{
					user = await _userManager.FindByEmailAsync(model.Email);
					if (user is null)
					{
						// Mapping From SignUpViewModel -> ApplicationUser
						user = new ApplicationUser()
						{
							UserName = model.UserName,
							FirstName = model.FirstName,
							LastName = model.LastName,
							Email = model.Email,
							IsAggree = model.IsAgree
						};

						var result = await _userManager.CreateAsync(user, model.Password);
						if (result.Succeeded)
						{
							return RedirectToAction("SignIn");
						}
						foreach (var error in result.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
					}
					else 
					{
						ModelState.AddModelError(string.Empty, "Email is already exits (:");
					}
					return View();
				}
				ModelState.AddModelError(string.Empty, "UserName is already exits (:");
			}
			return View();

		}

		[HttpGet]
		public IActionResult SignIn() 
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid) 
			{
				try 
				{
					var user = await _userManager.FindByEmailAsync(model.Email); // To Sure If Email existing
					if (user is not null)
					{
						var flag = await _userManager.CheckPasswordAsync(user, model.Password); // To Check The Password is True
						if (flag)
						{
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false); //Generate Token For U According to RemeberMe value
							if (result.Succeeded)
							{
								return RedirectToAction("Index", "Home");
							}
						}
					}
					ModelState.AddModelError(string.Empty, "Invalid Login");
				}
				catch (Exception ex) 
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
				
			}
			return View();
		}

		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendRestPasswordUrl(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				{
					if (user is not null) 
					{ 
						var token = await _userManager.GeneratePasswordResetTokenAsync(user);
						// Create Reset Password Url
						var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token},Request.Scheme);

						var email = new EmailFormat()
						{
							To = model.Email,
							Subject = "Reset Password",
							Body = url
						};
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Operation Please Try Again");
			}
			return View(model);
		}
	}
}
