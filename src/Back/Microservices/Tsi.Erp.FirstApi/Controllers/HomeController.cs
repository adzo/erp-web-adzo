using Microsoft.AspNetCore.Mvc;

namespace Tsi.Erp.FirstApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
