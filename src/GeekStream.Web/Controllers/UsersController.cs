using System.Linq;
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
        [Route("[controller]/{id}")]
        public IActionResult UserProfile(string id)
        {
            var userViewModel = _userService.GetUserById(id);
            userViewModel.Articles = _articleService.FindByAuthorId(id).ToList();
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }
    }
}
