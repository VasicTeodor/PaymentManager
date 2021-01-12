using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentCardCentre.Service.Dto;
using PaymentCardCentre.Service.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentCardCentreController : ControllerBase
    {
        private readonly IPCCPayment _pccPayment;
        private readonly IMapper _mapper;

        public PaymentCardCentreController(IPCCPayment pccPayment, IMapper mapper)
        {
            _pccPayment = pccPayment;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("PersistPayment")]
        public async Task<ActionResult<ResponseDto>> CheckPayment([FromBody] RequestDto request)
        {
            Log.Information($"PCC received request");
            var paymentResponse = await _pccPayment.Payment(request);
            return Ok(paymentResponse);
        }
    }
}
