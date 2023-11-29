using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }
    }
}
