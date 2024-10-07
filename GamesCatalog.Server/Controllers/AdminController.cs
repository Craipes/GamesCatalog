using GamesCatalog.Server.ViewModels;

namespace GamesCatalog.Server.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
