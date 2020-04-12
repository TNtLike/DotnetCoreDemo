using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi
{
    public class ServiceController : ControllerBase
    {
        private readonly CarService _carservice;
        private readonly QRCodeService _qrservice;
        public ServiceController(CarService carserver, QRCodeService qrservice)
        {
            _carservice = carserver;
            _qrservice = qrservice;
        }

        [HttpGet]
        public IActionResult Cars(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                List<Car> listmsg = _carservice.GetTs();
                if (listmsg.Count <= 0)
                {
                    return NotFound();
                }
                return Ok(listmsg);
            }
            else
            {
                Car rtnmsg = _carservice.GetT(id);
                if (rtnmsg == null)
                {
                    return NotFound();
                }
                return Ok(rtnmsg);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Cars([FromBody] Car car)
        {
            var rtnmsg = await _carservice.CreateAsync(car);
            return Ok(rtnmsg);
        }

        [HttpPut]
        public async Task<IActionResult> Cars(string id, [FromBody] Car car)
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
