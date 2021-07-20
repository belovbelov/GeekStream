using GeekStream.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekStream.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }
    }
}
