﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using application.Models;
using application.Context;
using Microsoft.EntityFrameworkCore;

namespace application.Controllers {
    public class HomeController : Controller {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ApplicationContext _db;

        public HomeController(ApplicationContext db) {
            _db = db;
        }

        public async Task<IActionResult> Index() {
            var member = from m in _db.Member select m;
            var list = await member.ToListAsync();

            _logger.Info("list :: " + list.ToString());

            return View();
        }
    }
}