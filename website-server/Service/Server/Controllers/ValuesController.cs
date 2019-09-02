using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/Values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/Values/get_id
        [HttpPost("get_id")]
        public string GetId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
