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
        public IActionResult Cars()
        {
            List<Car> rtnmsg = _carservice.GetTs();
            if (rtnmsg.Count <= 0)
            {
                return NotFound();
            }
            return Ok(rtnmsg);
        }

        [HttpGet]
        public IActionResult Car(string id)
        {
            Car rtnmsg = _carservice.GetT(id);
            if (rtnmsg == null)
            {
                return NotFound();
            }
            return Ok(rtnmsg);
        }

        [HttpPost]
        public async Task<IActionResult> Car([FromBody] Car car)
        {
            var rtnmsg = await _carservice.CreateAsync(car);
            return Ok(rtnmsg);
        }

        [HttpPut]
        public async Task<IActionResult> Car(string id, [FromBody] Car car)
        {
            var rtnmsg = await _carservice.UpdateAsync(id, car);
            return Ok(rtnmsg);
        }

        [HttpGet]
        public IActionResult CarCode(string id)
        {
            Code codeIn = _qrservice.GetCarCode(id);
            if (codeIn == null)
            {
                Car carIn = _carservice.GetT(id);
                codeIn = _qrservice.InitCode(id, carIn.CarTypeName, 4);
            }
            return Ok(codeIn);
        }
    }
}
