
using Microsoft.AspNetCore.Mvc;


namespace MyApp.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

     
    }
}
