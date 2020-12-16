using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via PaymentCard", "PaymentCard.Service", port.Value.ToString() }));
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<IEnumerable<string>> GetById(string id)
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via PaymentCard", "PaymentCard.Service", $"ID received {id}", port.Value.ToString() }));
        }
    }
}