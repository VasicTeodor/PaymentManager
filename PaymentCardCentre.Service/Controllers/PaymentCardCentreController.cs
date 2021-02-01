using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentCardCentre.Service.Dto;
using PaymentCardCentre.Service.Repository;
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
        private readonly IUnitOfWork _unitOfWork;

        public PaymentCardCentreController(IPCCPayment pccPayment, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _pccPayment = pccPayment;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("PersistPayment")]
        public async Task<ActionResult<ResponseDto>> CheckPayment([FromBody] RequestDto request)
        {
            Log.Information($"PCC received request");
            var paymentResponse = await _pccPayment.Payment(request);
            return Ok(paymentResponse);
        }

        [HttpPost]
        [Route("RegisterBank")]
        public async Task<ActionResult<BankRegisterResponseDto>> RegisterBankToPcc([FromBody] BankRegisterRequestDto requestDto)
        {
            Log.Information($"PCC Request for register bank received");
            var regResponse = await _unitOfWork.Banks.AddBankByPan(requestDto.BankPanUrl);
            if(regResponse > 0)
            {
                return Ok(new BankRegisterResponseDto() {Status = "SUCCESS" });
            }else
            {
                return Ok(new BankRegisterResponseDto() { Status = "ERROR" });
            }
        }
    }
}
