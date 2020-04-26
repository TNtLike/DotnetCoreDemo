using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;
using MyWebApi.Services;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody]SignInRequest req)
        {
            var rtnmsg = await _userservice.GetUserAsync(req);
            if (rtnmsg == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(rtnmsg);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequest user)
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
