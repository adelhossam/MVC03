using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Company.G03.PL.Helpers;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Company.G03.PL.Controllers
{
	[Authorize]
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

        public async Task<IActionResult> Index(string SearchInput)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees =await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
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
        public async Task<IActionResult> Create() 
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(); // Extra Data So we Use ViewData
            ViewData["Departments"] = departments;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid) 
            {

                model.ImageName = DecumentSettings.UploadFile(model.Image,"images");
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

                var count = await _unitOfWork.EmployeeRepository.AddAsync(employee);
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

        public async Task<IActionResult> Details(int?id , string ViewName = "Details") 
        {
            if (id is null) return BadRequest();
            var emp = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            var employee = _mapper.Map<EmployeeViewModel>(emp);
            if(employee is null) return NotFound();

            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Update (int? id)
        {
            //if (id is null) return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null) return NotFound();
            //return View(employee);

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(); // Extra Data
            ViewData["Departments"] = departments;
            return await Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute]int? id , EmployeeViewModel model)
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
                    if (model.ImageName is not null)
                    {
                        DecumentSettings.Delete(model.ImageName, "images");
                    }
                    model.ImageName= DecumentSettings.UploadFile(model.Image, "images");

                    var employee = _mapper.Map<Employee>(model); // Cast model To Employee

                    var Count = await _unitOfWork.EmployeeRepository.UpdateAsync(employee);
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
        public async Task<IActionResult> Delete(int? id) 
        {
            //if (id is null) return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null) return NotFound();
            //return View(employee);
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int? id,EmployeeViewModel model)
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
                    var count = await _unitOfWork.EmployeeRepository.DeleteAsync(employee);
                    if (count > 0)
                    {
                        DecumentSettings.Delete(model.ImageName, "images");
                        return RedirectToAction("Index");
                    }
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
