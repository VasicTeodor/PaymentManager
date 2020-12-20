using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingCompany.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        public SettingsController()
        {

        }

        [HttpGet]
        [Route("getwebstore")]
        public async Task<IActionResult> GetWebStoreInfo()
        {
            return Ok(new { WebStoreId= "54a0924b-200a-4efc-a6ac-ff21873c3b37", WebStoreName= "PublishingCompany01" });
        }
    }
}
