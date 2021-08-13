using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using GeekStream.Core.Entities;
using GeekStream.Core.Services;
using GeekStream.Core.ViewModels;
using GeekStream.Core.ViewModels.Article;
using GeekStream.Core.ViewModels.Comment;


namespace GeekStream.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticleService _articleService;
        private readonly CategoryService _categoryService;
        private readonly CommentService _commentService;
        private readonly UserService _userService;

        public ArticlesController(ArticleService articleService, CategoryService categoryService,
            CommentService commentService, UserService userService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string searchString = null)
        {
            IEnumerable<ArticleFeedViewModel> articles;

            if (searchString == null)
            {
                articles = _articleService.GetAllArticles();
                return View(articles);
            }

            articles = _articleService.FindByKeywords(searchString);
            return View(articles);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            await _articleService.Approve(id);
            return RedirectToAction("Pending", "Articles");
        }

        [HttpPost]
        public async Task<IActionResult> Post(int id)
        {
            await _articleService.Post(id);

            return RedirectToAction("Details", "Articles", new {id = id});
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var article = _articleService.GetArticleById(id);
            var articleViewModel = new ArticleDetailsViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                PublishedDate = article.PostedOn,
                Author = article.Author.FirstName + " " + article.Author.LastName,
                AuthorId = article.Author.Id,
                Category = article.Category.Name,
                CategoryId = article.CategoryId,
                Rating = article.Rating,
                Images = article.Images,
                Comments = article.Comments
                    .Select(c => new CommentsViewModel
                {
                    Id = c.Id,
                    UserName = c.UserName,
                    Content = c.Content,
                    Rating = c.Rating
                }),
                UserIcon = article.Author.Avatar,
                CategoryIcon = article.Category.Image
            };

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

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Drafts()
        {
            var articles = _articleService.GetDrafts();
            return View(articles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleEditViewModel model, string action,
            IFormFileCollection files = null)
        {
            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");
                    foreach (var file in files)
                    {
                        var image = new FilePath
                        {
                            FileName = Guid.NewGuid() + Path.GetExtension(file.FileName),
                            FileType = FileType.Photo,
                        };
                        await using (Stream fileStream = new FileStream(path + image.FileName, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        model.FilePaths.Add(image);
                    }
                }

                await _articleService.SaveArticleAsync(model, action);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("[controller]/{id}/Edit")]
        public IActionResult Edit(int id)
        {
            var article = _articleService.GetArticleToEditById(id);

            if (article == null)
            {
                return NotFound();
            }

            ViewData["Category"] = new SelectList(_categoryService.GetAllCategories(), "Id", "Name");

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(ArticleEditViewModel model, string action,
            List<IFormFile> files = null)
        {
            if (ModelState.IsValid)
            {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");
                foreach (var file in files)
                {
                    var image = new FilePath
                    {
                        FileName = Guid.NewGuid() + Path.GetExtension(file.FileName),
                        FileType = FileType.Photo,
                    };
                        await using (Stream fileStream = new FileStream(path + image.FileName, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        model.FilePaths.Add(image);
                }

                await _articleService.UpdateArticleAsync(model, action);
            return RedirectToAction("Drafts", "Articles");
            }

            ViewData["Category"] = new SelectList(_categoryService.GetAllCategories(), "Id", "Name");
            return RedirectToAction("Drafts","Articles");
        }



        [HttpGet]
        [Route("[controller]/{id}/[action]")]
        public IActionResult Delete(int id)
        {
            var article = _articleService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            if (article.Author.Id != _userService.GetCurrentUser().Id)
            {
                return BadRequest();
            }

            if (article.Type == ArticleType.Posted)
            {
                return BadRequest();
            }


            return View(article);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Route("[controller]/{id}/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleService.DeleteArticleAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Comment(int articleId, string text)
        {
            await _commentService.LeaveComment(articleId, text);

            return NoContent();
        }

        [HttpGet]
        [Route("[controller]/Pending/{id}")]
        [Authorize(Roles = "Reviewer")]
        public IActionResult Review(int id)
        {
            var article = _articleService.GetArticleById(id);
            var articleViewModel = new ArticleReviewViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Author = article.Author.FirstName + " " + article.Author.LastName,
                AuthorId = article.Author.Id,
                Images = article.Images,
                UserIcon = article.Author.Avatar,
                CategoryIcon = article.Category.Image,
                Category = article.Category.Name,
                CategoryId = article.CategoryId,
                Comments = article.Comments
                    .Select(c => new CommentsViewModel
                {
                    Id = c.Id,
                    UserName = c.UserName,
                    Content = c.Content,
                    Rating = c.Rating
                }),
            };
            return View(articleViewModel);

        }

        [HttpGet]
        [Route("[controller]/Pending/")]
        [Authorize(Roles = "Reviewer")]
        public IActionResult Pending()
        {
            IEnumerable<ArticleFeedViewModel> articles;

            articles = _articleService.PendingArticles();
            if (articles == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(articles);
        }
    }
}
