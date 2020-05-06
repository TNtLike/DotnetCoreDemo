using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;
using MyWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MyWebApi
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ApiController : ControllerBase
    {
        private readonly CodeService _qrservice;
        private readonly JobService _jobservice;
        private readonly FileService _fileservice;
        public delegate HttpContext HttpRequestHandler(HttpContext context);

        public ApiController(CodeService qrservice, JobService jobservice, FileService fileservice)
        {
            _qrservice = qrservice;
            _jobservice = jobservice;
            _fileservice = fileservice;
        }

        [HttpGet]
        public IActionResult Jobs(int currpage = 1, int pagenum = 10)
        {
            List<Job> listmsg = _jobservice.Gets(currpage, pagenum);
            if (listmsg.Count <= 0)
            {
                return NotFound();
            }
            return Ok(listmsg);
        }


        [HttpGet("{id}")]
        public IActionResult Jobs(string id)
        {
            Job rtnmsg = _jobservice.Get(id);
            if (rtnmsg == null)
            {
                return NotFound();
            }
            return Ok(rtnmsg);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Files(IFormCollection FormData)
        {
            ServiceResponse rtnmsg = new ServiceResponse();
            await Task.Run(() =>
            {
                _fileservice.InitUserFile(FormData);
            });
            return Ok(rtnmsg);
        }
    }
}
