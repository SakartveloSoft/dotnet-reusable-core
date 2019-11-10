using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SakartveloSoft.API.Core;
using SakartveloSoft.API.Core.Logging;
using SakartveloSoft.Framework.TetsWebAppAuth.Models;

namespace SakartveloSoft.Framework.TetsWebAppAuth.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly IScopedLogger<HomeController> _logger;
        private IAPIContext apiContext;

        public HomeController(IScopedLogger<HomeController> logger, IAPIContext context)
        {
            _logger = logger;
            apiContext = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("protected")]
        [Authorize]
        public IActionResult ProtectedPage()
        {
            return View();
        }
    }
}
