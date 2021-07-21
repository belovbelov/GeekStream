using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeekStream.Core.Entities;
using GeekStream.Core.Services;
using GeekStream.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;

namespace GeekStream.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CategoryService _categoryService;
        private readonly ArticleService _articleService;

        public CategoriesController(AppDbContext context, CategoryService categoryService, ArticleService articleService)
        {
            _context = context;
            _categoryService = categoryService;
            _articleService = articleService;
        }

        // GET: Categories
        [HttpGet]
        [AllowAnonymous]
        [Route("{controller}/{category}")]
        public IActionResult Index(string category)
        {
            if (category != null)
            {
                var articles = _articleService.FindByCategoryId(category);
                return View(articles);
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
