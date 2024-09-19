using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Company.G03.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
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
					ModelState.AddModelError(string.Empty, "Email is already exits (:");
					return View();
				}
				ModelState.AddModelError(string.Empty, "UserName is already exits (:");
			}
			return View();

		}
	}
}
