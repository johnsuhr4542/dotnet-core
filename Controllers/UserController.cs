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
    public async Task<IActionResult> Login(string username, string password) {
      var savedUser = await (
        from u in _db.Member
        where u.Username == username
        select u
      ).FirstOrDefaultAsync();

      if (savedUser == null) {
        var result = new Dictionary<string, object> {{ "message", "unauthorized" }};
        return new JsonResult(result);
      }

      var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, savedUser.Username),
        new Claim("Roles", "Admin")
      };

      var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

      await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(identity)
      );

      return new JsonResult(new Dictionary<string, object> {{ "message", "ok" }});
    }
  }
}