using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SakartveloSoft.API.Core;
using SakartveloSoft.API.Core.Logging;

namespace TestWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IScopedLogger<WeatherForecastController> _logger;
        private readonly IAPIContext _apiContext;

        public WeatherForecastController(IScopedLogger<WeatherForecastController> logger, IAPIContext apiContext)
        {
            _logger = logger;
            _apiContext = apiContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public string Post([FromBody]JsonElement jsonInput)
        {
            _logger.Debug(((Object)jsonInput).ToString());
            return "OK";
        }
    }
}
