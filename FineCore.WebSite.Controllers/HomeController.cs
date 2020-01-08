using System.Diagnostics;
using FineCore.B.Interfaces;
using FineCore.WebSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FineCore.WebSite.Controllers {
    [Authorize]
    public partial class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IGeneralUser user;
        public HomeController(ILogger<HomeController> logger,IGeneralUser user) { _logger = logger; this.user = user; }

        public IActionResult Index() {
             
            

             //_logger.LogWarning($"{dbProvider}");

             var logined = user.CheckLogin("","","");

            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public string getMenuData(int userId,int parentId=0) {
            var user = new FineCore.B.GeneralUser();
            var logined = user.CheckLogin("", "", "");
            var userInf = user.CurrentUser;
            return Newtonsoft.Json.JsonConvert.SerializeObject(user.CurrentUser.Funcs);
        }
    }
}
