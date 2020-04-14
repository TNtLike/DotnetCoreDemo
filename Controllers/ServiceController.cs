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
        private readonly BookService _bookservice;
        private readonly QRCodeService _qrservice;
        public ServiceController(BookService bookservice, QRCodeService qrservice)
        {
            _bookservice = bookservice;
            _qrservice = qrservice;
        }

        [HttpGet]
        public IActionResult Cars(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                List<Book> listmsg = _bookservice.GetTs();
                if (listmsg.Count <= 0)
                {
                    return NotFound();
                }
                return Ok(listmsg);
            }
            else
            {
                Book rtnmsg = _bookservice.GetT(id);
                if (rtnmsg == null)
                {
                    return NotFound();
                }
                return Ok(rtnmsg);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Cars([FromBody] Book book)
        {
            var rtnmsg = await _bookservice.CreateAsync(book);
            return Ok(rtnmsg);
        }

        [HttpPut]
        public async Task<IActionResult> Cars(string id, [FromBody] Book book)
        {
            var rtnmsg = await _bookservice.UpdateAsync(id, book);
            return Ok(rtnmsg);
        }

        [HttpGet]
        public IActionResult CarCode(string id)
        {
            Code codeIn = _qrservice.GetUnionCode(id);
            if (codeIn == null)
            {
                Book bookIn = _bookservice.GetT(id);
                codeIn = _qrservice.InitCode(id, bookIn.Link, 4);
            }
            return Ok(codeIn);
        }
    }
}
