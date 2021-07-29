using GeekStream.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStream.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleService _articleService;
        private readonly UserService _userService;

        public HomeController(ArticleService articleService, UserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string? id = null)
        {
            if (User.Identity is {IsAuthenticated: true})
            {
                if (_userService.IsSubscribed(_userService.GetCurrentUser()))
                {
                    var allArticles = _articleService.FindBySubscription(id);
                    return View(allArticles);
                }
            }

            var articles = _articleService.GetAllArticles();
            return View(articles);
        }
    }
}
