using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWebApi
{
    public class WeatherController : ControllerBase
    {
        private readonly IWeather _weather;
        private readonly ILogger<WeatherController> _logger;
        public WeatherController(ILogger<WeatherController> logger)
        {
            _weather = new WeatherApp();
            _logger = logger;
        }

        [HttpGet]
        public IActionResult DateWeather(DateTime date)
        {
            WeatherModel rtnmsg = _weather.GetDateWeather(date, out string status);
            return Ok(rtnmsg);

        }
        [HttpGet]
        public IActionResult RangeDateWeather(DateTime start, DateTime end)
        {
            List<WeatherModel> rtnmsg = _weather.GetRangeDateWeather(start: start, end: end, out string status);
            return Ok(rtnmsg);
        }
    }
}
