using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSample.Controllers.Base;

namespace WebApiSample.Controllers
{
    public class ErrorController : MyBaseController
    {
        [HttpGet]
        public IActionResult Error()
        {
            return Ok();
        }
    }
}
