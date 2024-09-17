﻿using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context) 
        { }

        public IEnumerable<Employee> GetByName(string Name)
        {
           return _context.Employees.Where(E => E.Name.ToLower().Contains(Name.ToLower())).Include(E=>E.WorkFor);
        }
    }
}
