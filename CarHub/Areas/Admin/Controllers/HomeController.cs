using Microsoft.AspNetCore.Mvc;

namespace CarHub.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
