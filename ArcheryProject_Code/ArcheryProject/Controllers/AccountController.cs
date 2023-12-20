using ArcheryProject.Models;
using Microsoft.AspNetCore.Mvc;
//Home
//Login

namespace ArcheryProject.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LoginOrRegister()
        {
            return View(new LoginRegisterModel());
        }
    }
}
