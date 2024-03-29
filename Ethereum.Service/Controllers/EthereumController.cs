﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ethereum.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EthereumController : ControllerBase
    {
        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Ethereum", "Ethereum.Service", port.Value.ToString() }));
        }
    }
}
