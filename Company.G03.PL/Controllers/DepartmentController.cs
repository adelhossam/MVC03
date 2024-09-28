using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository) // Ask CLR To Create Object From DepartmentRepository 
        {
            _departmentRepository = departmentRepository; 
        }
        public async Task<IActionResult> Index()
        {
            var department = await _departmentRepository.GetAllAsync();
            return View(department);
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)  // To Sure The Data Annotation About this model Is Valid
            {
                var count = await _departmentRepository.AddAsync(model); // To Sure the model is Add To Database
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index)); // Redirect To The Previous Page
                }
            }
            return View(model); // If model is not Valid it will remain in the same page
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id , string ViewName = "Details")
        {
            if (id is null) return BadRequest(); // Erorr 400

            var department = await _departmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound(); //Erorr 404

            return View(ViewName, department);
        }
        public async Task<IActionResult> Update(int? id)
        {
            //if (id is null) return BadRequest();
            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return await Details(id, "Update"); // Refactor Code
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevent any Extenal Tool Like Postman To Call This Action 
        public async Task<IActionResult> Update([FromRoute]int? id,Department model) // [FromRoute] it means Get The value of id from Path or segment
        {
            try
            {
                if (id != model.Id) return BadRequest(); // To prevent any Modification in Front End and try to change diffrent model
                if (ModelState.IsValid)  // To Sure The Data Annotation About this model Is Valid
                {
                    var count = await _departmentRepository.UpdateAsync(model); // To Sure the model is Add To Database
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index)); // Redirect To The Previous Page
                    }
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message); // To Print The Exception In The View
            }
            
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest(); // Erorr 400

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound(); //Erorr 404

            //return View(department);
            return await Details(id, "Delete"); // Refactoring Code
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int? id,Department model) 
        {
            try 
            {
                if (id != model.Id) return BadRequest();
                var count = await _departmentRepository.DeleteAsync(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
    }
}
