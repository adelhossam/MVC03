using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context) // Ask CLR To Create Object From AppDbContext
        {
            _context = context;
        }
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department Get(int id)
        {
            // return _context.Departments.FirstOrDefualt(D=>D.Id == id)
            return _context.Departments.Find(id);
        }

        public int Add(Department entity)
        {
            _context.Departments.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(Department entity)
        {
            _context.Departments.Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            return _context.SaveChanges();

        }
    }
}
