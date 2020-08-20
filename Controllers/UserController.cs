using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using application.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace application.Controllers {
    public class UserController : Controller {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ApplicationContext _db;

        public UserController(ApplicationContext db) {
            _db = db;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password) {
            var savedUser = await (
            from u in _db.Member
            where u.Username == username
            select u
            ).FirstOrDefaultAsync();

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, savedUser.Username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, savedUser.Username),
                new Claim("Roles", "Admin")
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            result["message"] = "Ok";

            return new JsonResult(result);
        }
    }
}