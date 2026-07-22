using appTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace appTest.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Email == "admin@gmail.com" && model.Password == "admin1234")
                return RedirectToAction("Index", "Home");

            if (model.Email == "user@gmail.com" && model.Password == "user1234")
                return RedirectToAction("Index", "DashboardUser");

            ViewBag.Error = "Email ou mot de passe incorrect.";
            return View(model);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}
