using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SakartveloSoft.API.Core;
using SakartveloSoft.API.Core.Logging;
using SakartveloSoft.Framework.TetsWebAppAuth.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SakartveloSoft.Framework.TetsWebAppAuth.Controllers
{
    [Route("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorController : Controller
    {
        private IScopedLogger<ErrorController> _logger;
        private IAPIContext apiContext;
        public ErrorController(IScopedLogger<ErrorController> logger, IAPIContext context)
        {
            _logger = logger;
            apiContext = context;

        }
        [Route("403")]
        public IActionResult AccessForbiddenToContent()
        {
            return View(new ErrorViewModel
            {
                RequestId = apiContext.RequestId,
                RequestUrl = apiContext.RequestUrl
            });
        }
        [Route("404")]
        public IActionResult ContentNotFound()
        {
            return View(new ErrorViewModel {
                RequestId = apiContext.RequestId,
                RequestUrl = apiContext.RequestUrl
            });
        }
        [Route("500")]
        public IActionResult AppError()
        {
            return View(new ErrorViewModel
            {
                RequestId = apiContext.RequestId,
                RequestUrl = apiContext.RequestUrl
            });
        }
    }
}
