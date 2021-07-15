using System.Collections.Generic;
using System.Linq;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll()
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                });
        }
    }
}