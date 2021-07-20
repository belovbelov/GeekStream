using Microsoft.AspNetCore.Mvc;

namespace GeekStream.Web.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
