using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Services;
using GeekStream.Core.ViewModels;
using GeekStream.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStream.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly ArticleService _articleService;

        public UsersController(UserService userService, ArticleService articleService)
        {
            _userService = userService;
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string id)
        {
            if (ModelState.IsValid)
            {
                await _userService.SubscribeAsync(id);
                return RedirectToAction("Index", "Categories");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Unsubscribe( string id)
        {
            if (ModelState.IsValid)
            {
                await _userService.UnsubscribeAsync(id);
                return RedirectToAction("Index", "Categories");
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/{id}")]
        public IActionResult UserProfile(string id)
        {
            var user = _userService.GetUserById(id);
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                UserName = user.FirstName + " " + user.LastName,
                IsSubscribed = _userService.IsSubscribed(_userService.GetCurrentUser(), user.Id),
                UserMail = user.Email,
                Rating = user.Rating,
                Articles = _articleService.FindByAuthorId(id)
            };

            return View(userViewModel);
        }
    }
}
