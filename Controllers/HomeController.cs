using Microsoft.AspNetCore.Mvc;
using Msit147Site.Models;
using System.Diagnostics;

namespace Msit147Site.Controllers
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

        public IActionResult First()
        {
            return View();
        }

        public IActionResult AjaxEvent()
        {
            return View();

        }

        public IActionResult Register() {
            return View();
        }

        public IActionResult Address() {
            return View();
        }

        public IActionResult Promise()
        {
            return View();
        }

        public IActionResult Fetch()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult jQuery()
        {
            return View();
        }

        public IActionResult Partail1()
        {
            return PartialView();
        }
        public IActionResult Partail2()
        {
            ViewBag.message = "Hello Partail View From Action";
            return PartialView();
        }

        public IActionResult Privacy()
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