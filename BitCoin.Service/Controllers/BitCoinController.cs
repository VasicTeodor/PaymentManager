using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitCoin.Service.Dtos;
using BitCoin.Service.Models;
using BitCoin.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitCoin.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BitCoinController : ControllerBase
    {
        private readonly ICoingateService _coingateService;
        public BitCoinController(ICoingateService coingateService)
        {
            _coingateService = coingateService;
        }

        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via Bitcoin", "Bitcoin.Service", port.Value.ToString() }));
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<IEnumerable<string>> GetById(string id)
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via Bitcoin", "Bitcoin.Service", $"ID received {id}", port.Value.ToString() }));
        }

        [HttpPost]
        [Route("create-payment")]
        public async Task<IActionResult> CreatePayment(OrderDto order)
        {
            var orderResult = await _coingateService.CreatePayment(order);

            return Ok(orderResult);
        }
    }
}
