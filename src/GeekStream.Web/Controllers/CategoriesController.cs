using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GeekStream.Core.Services;
using GeekStream.Core.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;

namespace GeekStream.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly ArticleService _articleService;
        private readonly UserService _userService;

        public CategoriesController(CategoryService categoryService, ArticleService articleService, UserService userService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/{category}")]
        public IActionResult Index(int category)
        {
            var foundCategory = _categoryService.GetCategoryById(category);
            var categoryViewModel = new CategoryViewModel
            {
                Id = foundCategory.Id,
                Name = foundCategory.Name,
                IsSubscribed = _userService.IsSubscribed(_userService.GetCurrentUser(), foundCategory.Id.ToString()),
                Articles = _articleService.FindByCategoryId(category).ToList(),
                CategoryIcon = foundCategory.Image
            };
            return View(categoryViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Categories")]
        public IActionResult All()
        {
            var categories = _categoryService.GetAllCategories();

            return View(categories);
        }
    }
}
