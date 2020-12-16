using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Services.Interfaces;
using Serilog;

namespace PaymentManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IGenericRestClient _restClient;
        private readonly IPaymentService _paymentService;

        public PaymentController(IGenericRestClient restClient, IPaymentService paymentService)
        {
            _restClient = restClient;
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("paybypaymentcard")]
        public async Task<IActionResult> PayByPaymentBank(PaymentRequestDto paymentRequestDto)
        {
            Log.Information("Received payment request for payment by card");
            var paymentRequest =
                await _paymentService.GeneratePaymentRequest(paymentRequestDto.MerchantId, paymentRequestDto.Amount);

            var result = await _restClient.PostRequest<PaymentRequestResponseDto>("mock/url",paymentRequest);


            return Ok(result);
        }
    }
}
