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
    }
}
