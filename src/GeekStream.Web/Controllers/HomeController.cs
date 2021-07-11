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
        private readonly ILogger<HomeController> _logger;
        private readonly ArticleService _articleService;// TODO убрать потом

        public HomeController(ILogger<HomeController> logger, ArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = _articleService.GetArticles();
            return View(articles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
