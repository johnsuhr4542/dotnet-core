using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace application.Controllers {
  public class UserController : Controller {
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public IActionResult Index() {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password) {
      await Task.Run(() => {});

      _logger.Info($"username : {username}, password : {password}");

      return View();
    }
  }
}