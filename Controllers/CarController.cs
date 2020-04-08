using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;

namespace MyWebApi
{
    public class CarController : ControllerBase
    {
        private readonly ICar _car;
        private readonly ILogger<CarController> _logger;
        public CarController(ILogger<CarController> logger)
        {
            _car = new CarApp();
            _logger = logger;
        }

        [HttpGet]
        public IActionResult DateWeather(string id)
        {
            Car rtnmsg = _car.GetCar(id, out string status);
            return Ok(rtnmsg);
        }
        [HttpGet]
        public IActionResult RangeDateWeather()
        {
            List<Car> rtnmsg = _car.GetAllCar(out string status);
            return Ok(rtnmsg);
        }
    }
}
