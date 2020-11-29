using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PaymentCard.Service.Controllers
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

            return new string[] { "value1", "value2", port.Value.ToString() };
        }
    }
}