using System.Diagnostics;
using System.Threading.Tasks;
using GeekStream.Core.Services;
using GeekStream.Infrastructure.Data;
using GeekStream.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleService _articleService;// TODO убрать потом

        public HomeController(ArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var articles = _articleService.GetArticles();
            return View(articles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
