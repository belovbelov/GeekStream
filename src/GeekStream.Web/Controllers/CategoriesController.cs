using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeekStream.Core.Entities;
using GeekStream.Core.Services;
using GeekStream.Core.ViewModels;
using GeekStream.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;

namespace GeekStream.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CategoryService _categoryService;
        private readonly ArticleService _articleService;
        private readonly UserService _userService;

        public CategoriesController(AppDbContext context, CategoryService categoryService, ArticleService articleService, UserService userService)
        {
            _context = context;
            _categoryService = categoryService;
            _articleService = articleService;
            _userService = userService;
        }

        // GET: Categories
        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/{category}")]
        public IActionResult Index(int category)
        {
            if (category != null)
            {
                var foundCategory = _categoryService.GetCategoryById(category);
                var categoryViewModel = new CategoryViewModel
                {
                    Id = foundCategory.Id,
                    Name = foundCategory.Name,
                    IsSubscribed = _userService.IsSubscribed(_userService.GetCurrentUser(), foundCategory.Id.ToString())
                };
                categoryViewModel.Articles = _articleService.FindByCategoryId(category);
                return View(categoryViewModel);
            }
            return NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Categories")]
        public IActionResult All()
        {
            var categories = _categoryService.GetAllCategories();

            return View(categories);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
