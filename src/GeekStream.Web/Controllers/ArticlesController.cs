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

        // GET: Articles
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string searchString = null)
        {
            if (_userService.IsSubscribed(_userService.GetCurrentUser()))
            {
                var articleViewModels = _articleService.FindByKeywords(searchString);
                return View(articleViewModels);
            }
            return NotFound();
        }

        // GET: Articles/Details/5
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

        // GET: Articles/Create
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_categoryService.GetAllCategories(), "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleCreationViewModel model, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.Count > 0)
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
            }
            ViewData["Category"] = new SelectList(_categoryService.GetAllCategories(), "Id", "Name");
            return View(model);
        }

        // GET: Articles/Edit/5
        [Route("[controller]/{id}/[action]")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            // ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Email", article.AuthorId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,ShortDescription,PostedOn,AuthorId,Rating")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Email", article.AuthorId);
            return View(article);
        }

        // GET: Articles/Delete/5
        [HttpGet]
        [Route("[controller]/{id}/[action]")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [Route("[controller]/{id}/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
