using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FineCore.WebSite.Models;
using Microsoft.Extensions.Logging;

namespace FineCore.WebSite.Controllers {
    public partial class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) { _logger = logger; }

        public IActionResult Index() {
            _logger.LogWarning("Hi, 这是Log4net日志。");
            var dbProvider = FineCore.DB.DbSettings.GetDbProvider();
            _logger.LogWarning($"{dbProvider}");
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
