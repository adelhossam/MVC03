using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext contex)
        {
            _employeeRepository = new EmployeeRepository(contex);
            _departmentRepository = new DepartmentRepository(contex);
            _context = contex;
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository; // Syntax Suger with =>
        //OR public IEmployeeRepository EmployeeRepository { get { return _employeeRepository;}} 
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

    }
}
