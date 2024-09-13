using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Company.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)//ASK CLR To Create an Object From EmployeeController
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid) 
            {
                var count = _employeeRepository.Add(model);
                if (count > 0)
                    return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(int?id , string ViewName = "Details") 
        {
            if (id is null) return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if(employee is null) return NotFound();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Update (int? id)
        {
            //if (id is null) return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null) return NotFound();
            //return View(employee);
            return Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int? id , Employee model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = _employeeRepository.Update(model);
                    if (Count > 0)
                        return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int? id) 
        {
            //if (id is null) return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null) return NotFound();
            //return View(employee);
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int? id,Employee model)
        {
            try 
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var count = _employeeRepository.Delete(model);
                    if (count > 0)
                        return RedirectToAction("Index");
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
