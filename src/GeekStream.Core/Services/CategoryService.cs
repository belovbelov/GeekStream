using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels.Category;

namespace GeekStream.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserService _userService;

        public CategoryService(ICategoryRepository categoryRepository, UserService userService)
        {
            _categoryRepository = categoryRepository;
            _userService = userService;
        }
        public IEnumerable<CategoriesListViewModel> GetAllCategories()
        {
            return _categoryRepository.GetAll()
                .Select(category => new CategoriesListViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                });
        }
        public async Task UpdateCategory(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }
        public async Task SaveCategoryAsync(Category category)
        {
            await _categoryRepository.SaveAsync(category);
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public IEnumerable<CategoriesListViewModel> SubscribedCategories()
        {
            var userId = _userService.GetCurrentUser().Id;
            var categories = _categoryRepository.SubscribedOn(userId);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sub");
            var files = Directory.GetFiles(path)
                .Select(Path.GetFileName).ToArray();
            var random = new Random();
            return categories
                .Select(c => new CategoriesListViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                });
        }

        public async Task DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            await _categoryRepository.DeleteAsync(category);
        }
    }
}