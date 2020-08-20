using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using application.Models;
using application.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace application.Controllers {
    public class HomeController : Controller {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ApplicationContext _db;

        public HomeController(ApplicationContext db) {
            _db = db;
        }

        public IActionResult Index() {
            var loginUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (loginUser != null)
                _logger.Info($"login user :: {loginUser}");
            else
                _logger.Info("not logged-in");
            return View();
        }

        [Authorize(Policy = "MyPolicy")]
        public IActionResult UserInfo() {
            return new JsonResult(new Dictionary<string, string>{{ "message", "ok" }});
        }
    }
}
