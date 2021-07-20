using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public async Task SaveAsync(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public Category GetById(int id)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == id);
        }

        public async Task Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}