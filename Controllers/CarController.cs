using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;
using MyWebApi.Services;
using System.Text.Json;

namespace MyWebApi
{
    public class CarController : ControllerBase
    {
        private readonly CarService _carserver;
        public CarController(CarService carserver)
        {
            _carserver = carserver;
        }

        [HttpGet]
        public IActionResult Car(string id)
        {
            Car rtnmsg = _carserver.Get(id);
            return Ok(rtnmsg);
        }
        [HttpGet]
        public IActionResult AllCar()
        {
            List<Car> rtnmsg = _carserver.Get();
            return Ok(rtnmsg);
        }
        [HttpPost]
        public IActionResult Car([FromBody] Car car)
        {
            Car rtnmsg = _carserver.Create(car);
            return Ok(rtnmsg);
        }
    }
}
