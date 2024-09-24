using AutoMapper;
using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.G03.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager , IMapper mapper)
        {
			_userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchInput)
		{
			var users = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(SearchInput)) 
			{
				users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
			}
			else 
			{
				users = _userManager.Users.Where(U => U.Email.ToLower()
									.Contains(SearchInput.ToLower()))
									.Select(U=> new UserViewModel() 
									{
										Id = U.Id,
										FirstName = U.FirstName,
										LastName = U.LastName,
										Email = U.Email,
										Roles = _userManager.GetRolesAsync(U).Result
									});
			}
			return View(users);
		}

		public async Task<IActionResult> Details(string? id , string ViewName = "Details")
		{
			if (id is null)
				return BadRequest(); // Error 400

			var UserFromDb = await _userManager.FindByIdAsync(id);
			
			if (UserFromDb is null)
				return NotFound(); // Error 404

			//var User = _mapper.Map<UserViewModel>(UserFromDb);
			var User = new UserViewModel()
			{
				Id = UserFromDb.Id,
				FirstName = UserFromDb.FirstName,
				LastName = UserFromDb.LastName,
				Email = UserFromDb.Email,
				Roles = _userManager.GetRolesAsync(UserFromDb).Result
			};
			return View(ViewName,User);
		}

		[HttpGet]
		public async Task<IActionResult> Update (string? id)
		{
			return await Details(id, "Update");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update ([FromRoute] string? id , UserViewModel model)
		{
			if (id != model.Id)
				return BadRequest(); // Error 400

			if (ModelState.IsValid)
			{
                var UserFromDb = await _userManager.FindByIdAsync(id);
                if (UserFromDb is null)
                    return NotFound();  // Error 404

                UserFromDb.FirstName = model.FirstName;
                UserFromDb.LastName = model.LastName;
                UserFromDb.Email = model.Email;

                var result = await _userManager.UpdateAsync(UserFromDb);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string? id)
		{
			return await Details(id,"Delete");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            if (id != model.Id)
                return BadRequest(); // Error 400

            if (ModelState.IsValid)
            {
                var UserFromDb = await _userManager.FindByIdAsync(id);
                if (UserFromDb is null)
                    return NotFound();  // Error 404

               
                var result = await _userManager.DeleteAsync(UserFromDb);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


    }
}
