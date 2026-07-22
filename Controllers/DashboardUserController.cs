using Microsoft.AspNetCore.Mvc;

namespace appTest.Controllers
{
    public class DashboardUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
