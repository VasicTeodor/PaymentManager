using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentManager.Api.Repository.Interfaces;
using Serilog;

namespace PaymentManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServiceController : ControllerBase
    {
        private readonly IWebStoreRepository _repository;

        public PaymentServiceController(IWebStoreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getpaymentoptions")]
        public async Task<IActionResult> GetPaymentOptions([FromQuery] Guid webStoreId)
        {
            Log.Information($"Request for payment options for web store with id {webStoreId}");
            var webStore = await _repository.GetWebStoreById(webStoreId);
            var result = webStore.PaymentOptions.Select(po => po.PaymentService).ToList();

            return Ok(result);
        }
    }
}
