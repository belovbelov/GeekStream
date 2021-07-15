using System.Collections.Generic;
using System.Linq;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

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
    }
}