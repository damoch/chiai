using Microsoft.AspNetCore.Mvc;

namespace chiai.Server.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
