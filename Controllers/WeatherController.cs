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
            string status = string.Empty;
            WeatherModel rtnmsg = _weather.GetDateWeather(date, out status);
            if (status == "ok")
            {

            }
            else
            {

            }
            return Ok(rtnmsg);

        }
        [HttpGet]
        public IActionResult RangeDateWeather(DateTime start, DateTime end)
        {
            string status = string.Empty;
            List<WeatherModel> rtnmsg = _weather.GetRangeDateWeather(start, end, out status);
            if (status == "ok")
            {

            }
            else
            {

            }
            return Ok(rtnmsg);

        }
    }
}
