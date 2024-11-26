using MediaDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using PAWPMD.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace MediaDashboard.Controllers
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
            var roles = HttpContext.Items["UserRoles"] as List<string> ?? new List<string>();
            ViewBag.Roles = roles;
            
            return View();
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
