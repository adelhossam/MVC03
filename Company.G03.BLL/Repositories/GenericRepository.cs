using Company.G03.BLL.Interfaces;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _context; // Protected as childern can use it
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await _context.Employees.Include(E=>E.WorkFor).AsNoTracking().ToListAsync();
            }
            else
            {
                return _context.Set<T>().ToList();
            }
        }
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> AddAsync(T entity)
        {
            // _context.Set<T>().Add(entity);
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            //_context.Set<T>().Update(entity);
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            //_context.Set<T>().Remove(entity);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

    }
}
