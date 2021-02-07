using Issuer.Service.Dto;
using Issuer.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IssuerController : ControllerBase
    {
        private readonly IIssuerPaymentService _issuerPaymentService;
        //private readonly string _issuerUrl = "http://localhost:55208";
        private readonly string _pccUrl = "http://localhost:5080/PaymentCardCentre/PersistPayment";


        public IssuerController(IIssuerPaymentService issuerPaymentService)
        {
            _issuerPaymentService = issuerPaymentService;
        }

        [HttpPost("IssuerPayment/{url}")]
        //[Route("IssuerPayment")]
        public ActionResult<ResponseDto> IssuerPayment([FromBody] RequestDto issuerRequest, string url)
        {
            ResponseDto response = _issuerPaymentService.IssuerPayment(issuerRequest,url);
            return Ok(response);
        }
    }
}
