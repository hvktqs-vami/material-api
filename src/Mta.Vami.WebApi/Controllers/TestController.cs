using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]//Tạm thời để anymous để test
    public class TestController: ControllerBase
    {
        
        [HttpGet]
        [Route("[action]")]
        public IActionResult TestCors()
        {
            return Ok();
        }
    }
}
