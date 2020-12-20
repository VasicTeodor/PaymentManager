using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using PayPal.Service.Dtos;
using PayPal.Service.Services.Interfaces;

namespace PayPal.Service.Services
{
    public class PayPalService : IPayPalService
    {
        private string _accessToken;

        public IConfiguration Configuration { get; }

        public string Token
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        public PayPalService(IConfiguration configuration)
        {
            Configuration = configuration;
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

            try
            {
                Payment payment = new Payment()
                {
                    intent = "sale",
                    payer = new Payer() { payment_method = "paypal" },
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
                                        name = paymentRequest.NameOfBook,
                                        currency = paymentRequest.Currency,
                                        price = paymentRequest.Price.ToString("0.##", CultureInfo.InvariantCulture),
                                        sku = "sku",
                                        quantity = "1"
                                    }
                                }
                            },
                            amount = new Amount()
                            {
                                currency = paymentRequest.Currency,
                                total = paymentRequest.Price.ToString("0.##" ,CultureInfo.InvariantCulture)
                            },
                            description = paymentRequest.Description
                        }
                    },
                    redirect_urls = new RedirectUrls()
                    {
                        cancel_url = "https://localhost:5005/paypal/paypal/cancel",
                        return_url = $"https://localhost:5005/paypal/paypal/success"
                    }
                };

                createdPayment = await Task.Run(() => payment.Create(apiContext));

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

            Payment executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));

            return executedPayment;
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