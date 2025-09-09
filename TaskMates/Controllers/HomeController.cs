using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskMates.Models;

namespace TaskMates.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Ideas()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult PseudoLogin()
        {
            return View();
        }

        public IActionResult PseudoRegister()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
