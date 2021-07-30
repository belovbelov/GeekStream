using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeekStream.Core.Entities;
using GeekStream.Infrastructure.Data;
using GeekStream.Core.Services;
using GeekStream.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Microsoft.AspNetCore.Http;


namespace GeekStream.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ArticleService _articleService;
        private readonly CategoryService _categoryService;
        private readonly UserService _userService;

        public ArticlesController(AppDbContext context, ArticleService articleService, CategoryService categoryService, UserService userService)
        {
            _context = context;
            _articleService = articleService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string searchString = null)
        {
            IEnumerable<ArticleViewModel> articles;

            if (searchString == null)
            {
                articles = _articleService.GetAllArticles();
                return View(articles);
            }

            articles = _articleService.FindByKeywords(searchString);
            return View(articles);
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var articleViewModel = _articleService.GetArticleById(id);

            if (articleViewModel == null)
            {
                return NotFound();
            }

            return View(articleViewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_categoryService.GetAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleCreationViewModel model, List<IFormFile> files = null)
        {
            if (ModelState.IsValid)
            {
                    foreach (var file in files)
                    {
                        var image = new FilePath
                        {
                            FileName = System.IO.Path.GetFileName(file.FileName),
                            FileType = FileType.Photo,
                            User = _userService.GetCurrentUser()
                        };
                        model.FilePaths.Add(image);
                    }
                    await _articleService.SaveArticleAsync(model);
                    return RedirectToAction(nameof(Index));
                    
            }
            ViewData["Category"] = new SelectList(_categoryService.GetAllCategories(), "Id", "Name");
            return View(model);
        }

        [Route("[controller]/{id}/[action]")]
        public IActionResult Edit(int id)
        {
            var article = _articleService.GetArticleToEditById(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ArticleCreationViewModel model, List<IFormFile> files = null)
        {
            if (ModelState.IsValid)
            {
                    foreach (var file in files)
                    {
                        var image = new FilePath
                        {
                            FileName = System.IO.Path.GetFileName(file.FileName),
                            FileType = FileType.Photo,
                            User = _userService.GetCurrentUser()
                        };
                        model.FilePaths.Add(image);
                    }
                    _articleService.UpdateArticle(model);
                    return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_categoryService.GetAllCategories(), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        [Route("[controller]/{id}/[action]")]
        public async Task<IActionResult> Delete(int id)
        {

            var article = _articleService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Route("[controller]/{id}/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleService.DeleteArticle(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
