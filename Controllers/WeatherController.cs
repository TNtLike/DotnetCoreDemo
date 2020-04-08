using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;

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
        public IActionResult DateWeather(string id)
        {
            Car rtnmsg = _weather.GetCar(id, out string status);
            return Ok(rtnmsg);
        }
        [HttpGet]
        public IActionResult RangeDateWeather()
        {
            List<Car> rtnmsg = _weather.GetAllCar(out string status);
            return Ok(rtnmsg);
        }
    }
}
