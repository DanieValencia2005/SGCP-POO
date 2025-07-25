using Microsoft.AspNetCore.Mvc;
using SGCP_POO.Models;
using System.Diagnostics;

namespace SGCP_POO.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Actualizar()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RegistroAdministrador()
        {
            return View();
        }

        public IActionResult RegistroEstudiante()
        {
            return View();
        }

        public IActionResult PortalEstudiante()
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
