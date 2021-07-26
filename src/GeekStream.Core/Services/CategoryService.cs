using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;

namespace GeekStream.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public async Task SaveCategoryAsync(Category category)
        {
            await _categoryRepository.SaveAsync(category);
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public async Task UpdateCategory(Category category)
        {
            await _categoryRepository.Update(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            await _categoryRepository.Delete(category);
        }
    }
}