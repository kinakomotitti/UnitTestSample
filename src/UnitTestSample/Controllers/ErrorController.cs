using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTestSample.Controllers.Base;

namespace UnitTestSample.Controllers
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
