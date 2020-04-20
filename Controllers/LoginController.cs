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
        public async Task<IActionResult> SignIn(SignInRequest user)
        {
            await Task.Delay(100);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest user)
        {
            var rtnmsg = await _userservice.CreateAsync(user);
            if (rtnmsg == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(rtnmsg);
            }
        }
    }
}
