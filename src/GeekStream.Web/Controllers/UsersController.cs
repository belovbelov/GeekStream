using GeekStream.Core.Services;
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{controller}/{name}")]
        public IActionResult UserProfile(string name)
        {
            var userViewModel = _userService.GetUserByName(name);
            userViewModel.Articles = _articleService.FindByAuthorName(name);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }
    }
}
