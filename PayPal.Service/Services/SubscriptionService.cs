using PayPal.Api;
using PayPal.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;
using PayPal.Service.Data.Entities;
using PayPal.Service.Dtos;
using PayPal.Service.Helpers;
using PayPal.Service.Repository.Interfaces;
using Serilog;

namespace PayPal.Service.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public IConfiguration Configuration { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private string _accessToken;
        private APIContext _context;
        private readonly Dictionary<string, string> _payPalConfig;

        public SubscriptionService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            Configuration = configuration;
            _payPalConfig = GetPayPalCredentials();
            _accessToken = new OAuthTokenCredential(_payPalConfig).GetAccessToken();
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _context = new APIContext(_accessToken)
            {
                Config = GetPayPalCredentials()
            };
        }

        public async Task<Plan> CreateBillingPlan(BillingPlanRequestDto billingPlan)
        {
            Plan plan = CreateNewPlan(billingPlan);
            PaymentDefinition paymentDefinition = CreateNewPaymentDefinition(billingPlan);
            Currency currency = CreateNewCurrencyInformation(billingPlan, paymentDefinition);
            SetChargeModels(paymentDefinition, currency);
            SetPaymentDefinitions(plan, paymentDefinition);
            plan.merchant_preferences = CreateMerchantPreferences(billingPlan, currency);
            try
            {
                var createdPlan = plan.Create(_context);
                ActivatePlan(createdPlan);
                Log.Information($"Successfully created new billing plan with status: {plan.state}");

                var createdPlanDb = _mapper.Map<BillingPlanRequest>(billingPlan);
                createdPlanDb.WebStoreId = billingPlan.WebStoreId;
                createdPlanDb.BillingPlanId = createdPlan.id;

                await _unitOfWork.BillingPlanRequests.Add(createdPlanDb);
                await _unitOfWork.CompleteAsync();

                return createdPlan;
            }
            catch (PayPalException ex)
            {
                Log.Error($"Error while crating new billing plan {ex.Message}");
                throw new Exception();
            }
        }

        public async Task<Agreement> CreateSubscription(SubscriptionRequestDto subscriptionRequest)
        {
            var agreement = new Agreement()
            {
                id = Guid.NewGuid().ToString(),
                name = "Base Agreement",
                description = "Basic Agreement",
                start_date = DateTime.Now.AddDays(1).ToString("O")
            };

            var plan = new Plan()
            {
                id = subscriptionRequest.billingPlanId
            };

            agreement.plan = plan;

            var payer = new Payer()
            {
                payment_method = "PAYPAL"
            };
            agreement.payer = payer;

            var address = new ShippingAddress
            {
                line1 = "123 Second Street",
                city = "Saratoga",
                state = "CA",
                postal_code = "95070",
                country_code = "US"
            };
            agreement.shipping_address = address;

            try
            {
                agreement = agreement.Create(_context);
                Log.Information($"Agreement created, token received {agreement.token}");

                var createdSubscription = new SubscriptionRequest()
                {
                    Description = agreement.description,
                    ExecuteAgreementUrl = agreement.links.FirstOrDefault(l => l.href == "execute").href,
                    ExpressCheckoutUrl = agreement.links.FirstOrDefault(l => l.href == "approval_url").href,
                    Name = agreement.name,
                    WebStoreId = subscriptionRequest.WebStoreId,
                    BillingPlanId = subscriptionRequest.billingPlanId
                };

                await _unitOfWork.SubscriptionRequests.Add(createdSubscription);
                await _unitOfWork.CompleteAsync();

                return agreement;
            }
            catch (UnsupportedContentTypeException e)
            {
                Log.Error($"Encoding error while crating new subscription {e.Message}");
                throw;
            }
            catch (MalformedLineException e)
            {
                Log.Error($"URL Error while crating new subscription {e.Message}");
                throw;
            }
            catch (PayPalException e)
            {
                Log.Error($"Error while crating new subscription {e.Message}");
                throw;
            }
        }

        public async Task<Agreement> ExecuteSubscription(ExecuteSubscriptionDto executeSubscription)
        {
            try
            {
                var agreement = new Agreement(){token = executeSubscription.Token};
                var result = agreement.Execute(_context);
                Log.Information($"Successfully executed subscription with token '{executeSubscription.Token}'");

                var executedSubscription = new ExecutedSubscription()
                {
                    WebStoreId = executeSubscription.WebStoreId,
                    UserId = executeSubscription.UserId,
                    Token = executeSubscription.Token
                };

                await _unitOfWork.ExecutedSubscriptions.Add(executedSubscription);
                await _unitOfWork.CompleteAsync();

                return result;
            }
            catch (PayPalException e)
            {
                Log.Error($"Error while executing subscription with token '{executeSubscription.Token}': {e.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<SubscriptionRequest>> GetSubscriptions(GetSubscriptionsDto getSubscriptions)
        {
            var subs = await _unitOfWork.SubscriptionRequests.GetAll();
            return subs.Where(s => s.WebStoreId == getSubscriptions.WebStoreId).ToList();
        }

        private void ActivatePlan(Plan plan)
        {
            var patch = new Patch()
            {
                path = "/",
                value = new Plan() { state = "ACTIVE" },
                op = "replace"
            };
            var patchRequest = new PatchRequest()
            {
                patch
            };
            plan.Update(_context, patchRequest);
        }

        private Plan CreateNewPlan(BillingPlanRequestDto request)
        {
            var plan = new Plan();
            plan.name = request.Name;
            plan.description = request.Description;
            plan.type = request.SubscriptionType.ToString();
            return plan;
        }

        private PaymentDefinition CreateNewPaymentDefinition(BillingPlanRequestDto request)
        {
            var paymentDefinition = new PaymentDefinition()
            {
                name = "Regular Payments",
                type = request.PaymentType.ToString(),
                frequency = request.Frequency.ToString(),
                frequency_interval = request.FrequencyInterval.ToString(),
                cycles = request.Cycles.ToString()
            };

            return paymentDefinition;
        }

        private Currency CreateNewCurrencyInformation(BillingPlanRequestDto request,
            PaymentDefinition paymentDefinition)
        {
            var currency = new Currency()
            {
                currency = request.Currency,
                value = request.Amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
            };
            paymentDefinition.amount = currency;
            return currency;
        }

        private void SetChargeModels(PaymentDefinition paymentDefinition, Currency currency)
        {
            ChargeModel chargeModel = new ChargeModel()
            {
                amount = currency,
                type = ChargeModelType.SHIPPING.ToString()
            };
            paymentDefinition.charge_models = new List<ChargeModel>(){chargeModel};
        }

        private void SetPaymentDefinitions(Plan plan, PaymentDefinition paymentDefinition)
        {
            plan.payment_definitions = new List<PaymentDefinition>(){paymentDefinition};
        }

        private MerchantPreferences CreateMerchantPreferences(BillingPlanRequestDto request, Currency currency)
        {
            var merchantPreferences = new MerchantPreferences()
            {
                setup_fee = currency,
                cancel_url = request.FailedUrl,
                return_url = request.SuccessUrl,
                max_fail_attempts = "0",
                auto_bill_amount = "YES",
                initial_fail_amount_action = FailAction.CANCEL.ToString()
            };
            return merchantPreferences;
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
