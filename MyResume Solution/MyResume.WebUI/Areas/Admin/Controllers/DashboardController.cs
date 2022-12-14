using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyResume.WebUI.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "sa")]
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
