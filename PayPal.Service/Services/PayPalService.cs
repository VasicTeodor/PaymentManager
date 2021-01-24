using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using PayPal.Service.Data.Entities;
using PayPal.Service.Dtos;
using PayPal.Service.Repository.Interfaces;
using PayPal.Service.Services.Interfaces;
using Serilog;

namespace PayPal.Service.Services
{
    public class PayPalService : IPayPalService
    {
        private string _accessToken;
        private readonly IPaymentRequestRepository _repository;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }

        public string Token
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        public PayPalService(IConfiguration configuration, IPaymentRequestRepository repository, IMapper mapper)
        {
            Configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Payment> CreatePayment(PaymentRequestDto paymentRequest)
        {
            Payment createdPayment = new Payment();
            var config = GetPayPalCredentials();
            _accessToken = new OAuthTokenCredential(config).GetAccessToken();

            var apiContext = new APIContext(_accessToken)
            {
                Config = GetPayPalCredentials()
            };

            var id = Guid.NewGuid().ToString();

            try
            {
                Payment payment = new Payment()
                {
                    id = id,
                    intent = paymentRequest.PaymentIntent,
                    payer = new Payer() { payment_method = paymentRequest.PaymentMethod },
                    transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            item_list = new ItemList()
                            {
                                items = new List<Item>()
                                {
                                    new Item()
                                    {
                                        description = paymentRequest.Description,
                                        name = "PayPal payment by PaymentManager",
                                        currency = paymentRequest.Currency,
                                        price = paymentRequest.Amount.ToString("0.##", CultureInfo.InvariantCulture),
                                        sku = "sku",
                                        quantity = "1"
                                    }
                                }
                            },
                            amount = new Amount()
                            {
                                currency = paymentRequest.Currency,
                                total = paymentRequest.Amount.ToString("0.##" ,CultureInfo.InvariantCulture)
                            },
                            description = paymentRequest.Description
                        }
                    },
                    redirect_urls = new RedirectUrls()
                    {
                        cancel_url = paymentRequest.ErrorUrl,
                        return_url = paymentRequest.SuccessUrl
                    }
                };

                createdPayment = await Task.Run(() => payment.Create(apiContext));

                var paymentDb = _mapper.Map<PaymentRequest>(paymentRequest);

                paymentDb.PaymentId = createdPayment.id;

                await _repository.SaveRequest(paymentDb);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return null;
            }

            return createdPayment;
        }

        public async Task<Payment> ExecutePayment(string paymentId, string payerId, string email = null)
        {
            var config = GetPayPalCredentials();
            _accessToken = new OAuthTokenCredential(config).GetAccessToken();

            var apiContext = new APIContext(_accessToken)
            {
                Config = GetPayPalCredentials()
            };

            PaymentExecution paymentExecution = new PaymentExecution() { payer_id = payerId };

            Payment payment = new Payment() { id = paymentId };
            try
            {
                Payment executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));
                return executedPayment;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

        private Dictionary<string, string> GetPayPalCredentials()
        {
            var mode = Configuration.GetSection("PayPalCredentials:mode").Value;
            var clientId = Configuration.GetSection("PayPalCredentials:clientId").Value;
            var clientSecret = Configuration.GetSection("PayPalCredentials:clientSecret").Value;

            return new Dictionary<string, string>()
            {
                {"mode", mode },
                {"clientId", clientId },
                {"clientSecret", clientSecret }
            };
        }
    }
}