using Microsoft.AspNetCore.Mvc;

namespace UbikeWeb.Controllers
{
    public class UbikeController : Controller
    {
        public IActionResult Index()
        {
            
            return View("QryPage");
        }
    }
}
