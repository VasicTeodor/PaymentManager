using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebStoreController : ControllerBase
    {
        // GET api/values  
        [HttpGet]
        [Route("Get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via PayPal", "PayPal.Service", port.Value.ToString() }));
        }
    }
}
