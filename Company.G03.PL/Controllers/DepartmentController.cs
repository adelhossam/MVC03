using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
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
    }
}
