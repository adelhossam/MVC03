using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Company.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper 
            )
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string SearchInput)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetByName(SearchInput);
            }
            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            //string Message = "Hello World";
            ////View's Dectionary : Use To Transfer Extra Data From Action To View [One Way]
            ////1. ViewData : Property Inherited From Controller - Dectionary
            //// Require Casting

            //ViewData["Message"] = Message + " From ViewData";

            ////2. ViewBag  : Property Inherited From Controller - dynamic
            //// Not Require Casting

            //ViewBag.Message01 = Message + " From ViewBag";

            ////3. TempData : Property Inherited From Controller - Dectionary
            //// Require Casting

            //TempData["Message02"] = Message + " From TempData";


            return View(result);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll(); // Extra Data So we Use ViewData
            ViewData["Departments"] = departments;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid) 
            {
                //Casting EmployeeViewModel -> Employee
                //Manuall Casting
                //var employee = new Employee() 
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DateOfCreation = model.DateOfCreation,
                //    HiringDate = model.HiringDate,
                //    WorkForId = model.WorkForId,
                //};

                // Atuo Mapping
                var employee = _mapper.Map<Employee>(model); // Cast model To Employee

                var count = _unitOfWork.EmployeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created Successfully";
                }
                else
                {
                    TempData["Message"] = "Employee Is Not Created Successfully";
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(int?id , string ViewName = "Details") 
        {
            if (id is null) return BadRequest();
            var emp = _unitOfWork.EmployeeRepository.Get(id.Value);
            var employee = _mapper.Map<EmployeeViewModel>(emp);
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

            var departments = _unitOfWork.DepartmentRepository.GetAll(); // Extra Data
            ViewData["Departments"] = departments;
            return Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int? id , EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {

                    //Casting EmployeeViewModel -> Employee
                    //Manuall Casting

                    //var employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Age = model.Age,
                    //    Address = model.Address,
                    //    Salary = model.Salary,
                    //    PhoneNumber = model.PhoneNumber,
                    //    Email = model.Email,
                    //    IsActive = model.IsActive,
                    //    IsDeleted = model.IsDeleted,
                    //    DateOfCreation = model.DateOfCreation,
                    //    HiringDate = model.HiringDate,
                    //    WorkForId = model.WorkForId,
                    //};
                    var employee = _mapper.Map<Employee>(model); // Cast model To Employee

                    var Count = _unitOfWork.EmployeeRepository.Update(employee);
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
        public IActionResult Delete([FromRoute]int? id,EmployeeViewModel model)
        {
            try 
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {

                    //Casting EmployeeViewModel -> Employee
                    //Manuall Casting

                    //var employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Age = model.Age,
                    //    Address = model.Address,
                    //    Salary = model.Salary,
                    //    PhoneNumber = model.PhoneNumber,
                    //    Email = model.Email,
                    //    IsActive = model.IsActive,
                    //    IsDeleted = model.IsDeleted,
                    //    DateOfCreation = model.DateOfCreation,
                    //    HiringDate = model.HiringDate,
                    //    WorkForId = model.WorkForId,
                    //};
                    var employee = _mapper.Map<Employee>(model); // Cast model To Employee
                    var count = _unitOfWork.EmployeeRepository.Delete(employee);
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
