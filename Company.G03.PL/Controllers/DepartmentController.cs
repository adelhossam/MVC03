using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository) // Ask CLR To Create Object From DepartmentRepository 
        {
            _departmentRepository = departmentRepository; 
        }
        public IActionResult Index()
        {
            var department = _departmentRepository.GetAll();
            return View(department);
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)  // To Sure The Data Annotation About this model Is Valid
            {
                var count = _departmentRepository.Add(model); // To Sure the model is Add To Database
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index)); // Redirect To The Previous Page
                }
            }
            return View(model); // If model is not Valid it will remain in the same page
        }

        public IActionResult Detatils(int? id)
        {
            if (id is null) return BadRequest(); // Erorr 400

            var department = _departmentRepository.Get(id.Value);

            if (department is null) return NotFound(); //Erorr 404

            return View(department);
        }
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevent any Extenal Tool Like Postman To Call This Action 
        public IActionResult Update([FromRoute]int? id,Department model) // [FromRoute] it means Get The value of id from Path or segment
        {
            try
            {
                if (id != model.Id) return BadRequest(); // To prevent any Modification in Front End and try to change diffrent model
                if (ModelState.IsValid)  // To Sure The Data Annotation About this model Is Valid
                {
                    var count = _departmentRepository.Update(model); // To Sure the model is Add To Database
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
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest(); // Erorr 400

            var department = _departmentRepository.Get(id.Value);

            if (department is null) return NotFound(); //Erorr 404

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int? id,Department model) 
        {
            try 
            {
                if (id != model.Id) return BadRequest();
                var count = _departmentRepository.Delete(model);
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
