using System.Net.Mime;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace application.Controllers {
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        [Route("get")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetMthod() {
            var map = new Dictionary<string, object>();
            map["message"] = "ok";

            return Ok(map);
        }

        [HttpPost]
        [Route("post")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Text.Plain)]
        public IActionResult FormRequest([FromForm] FormBody body) {
            _logger.Info($"age is null ? {body.Age == null}");
            _logger.Info($"name is null ? {body.Name == null}");

            _logger.Info($"body [Age : {body.Age}, Name : {body.Name}]");

            return Ok("ok");
        }

        [HttpPost]
        [Route("json")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Text.Plain)]
        public IActionResult JsonRequest([FromBody] FormBody body) {
            _logger.Info($"result : {body}");

            return Ok("ok");
        }
    }

    public class FormBody {
        public int? Age { get; set; }
        public string Name { get; set; }
    }
}