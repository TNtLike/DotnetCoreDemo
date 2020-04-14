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
    public class LoginController : ControllerBase
    {
        private readonly UserService _userservice;
        public LoginController(UserService userservice)
        {
            _userservice = userservice;
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            await Task.Delay(100);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> LogUp()
        {
            await Task.Delay(100);
            return NotFound();
        }
    }
}
