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
        private readonly CarService _carservice;
        private readonly QRCodeService _qrservice;
        public CarController(CarService carserver, QRCodeService qrservice)
        {
            _carservice = carserver;
            _qrservice = qrservice;
        }

        [HttpGet]
        public IActionResult Car(string id)
        {
            Car rtnmsg = _carservice.Get(id);
            return Ok(rtnmsg);
        }
        [HttpGet]
        public IActionResult AllCar()
        {
            List<Car> rtnmsg = _carservice.Get();
            return Ok(rtnmsg);
        }
        [HttpPost]
        public IActionResult Car([FromBody] Car car)
        {
            Car rtnmsg = _carservice.Create(car);
            return Ok(rtnmsg);
        }

        [HttpGet]
        public IActionResult CarCode(string id)
        {
            Code codeIn = _qrservice.GetCarCode(id);
            if (codeIn == null)
            {
                Car carIn = _carservice.Get(id);
                codeIn = _qrservice.InitCode(id, carIn.CarTypeName, 4);
            }
            return Ok(codeIn);
        }
    }
}
