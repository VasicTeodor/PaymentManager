using BitCoin.Service.Models;
using BitCoin.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using BitCoin.Service.Repository.Interfaces;
using AutoMapper;
using BitCoin.Service.Dtos;
using Serilog;

namespace BitCoin.Service.Services
{
    public class CoingateService : ICoingateService
    {
        public string _api_key, _api_secret, _app_id;
        private readonly int _nonce;
        private readonly string _baseUri;
        private readonly HttpClient _client;
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        //private readonly IGenericRestClient _genericRestClient;
        private readonly IConfiguration _configuration;
        public CoingateService(IConfiguration configuration, IOrderRepository repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
            _client = new HttpClient();
            _api_key = _configuration.GetSection("CoingateToken").Value;
            _api_secret = _configuration.GetSection("ApiSecret").Value;
            _app_id = _configuration.GetSection("AppId").Value;
            _baseUri = _configuration.GetSection("CoingateApiUrl").Value;
            _nonce = DateTime.Now.Second;
        }

        public async Task<dynamic> CreatePayment(OrderDto order)
        {
            string resourcePath = "/v2/orders/";

            _client.BaseAddress = new Uri(_baseUri);
            ConfigureHeaders(Signature());

            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("order_id", order.OrderId),
                new KeyValuePair<string, string>("price_amount", order.PriceAmount.ToString()),
                new KeyValuePair<string, string>("price_currency", order.PriceCurrency),
                new KeyValuePair<string, string>("receive_currency", order.ReceiveCurrency),
                new KeyValuePair<string, string>("title", order.Title),
                new KeyValuePair<string, string>("description", order.Description),
                new KeyValuePair<string, string>("callback_url", order.CallbackUrl),
                new KeyValuePair<string, string>("cancel_url", order.CancelUrl),
                new KeyValuePair<string, string>("success_url", order.SuccessUrl)
            });

            var response = await _client.PostAsync(resourcePath, body);
            if (!response.IsSuccessStatusCode)
            {
                Log.Information("Payment creation declined.");
                return HttpStatusCode.BadRequest;
            }
            var result = await JsonSerializer.DeserializeAsync<OrderResultDto>(await response.Content.ReadAsStreamAsync());

            var orderResultDb = _mapper.Map<OrderResult>(result);
            var orderDb = _mapper.Map<Order>(order);

            await _repository.SaveOrderResult(orderResultDb);
            await _repository.SaveOrder(orderDb);

            Log.Information("Payment created successfully.");

            return result;
        }

        private string ConfigureHmac(string key, string message)
        {
            var encoding = Encoding.UTF8;
            var keyByte = encoding.GetBytes(key);
            var hmac = new HMACSHA256(keyByte);
            var hash = hmac.ComputeHash(encoding.GetBytes(message));
            var sb = new StringBuilder(hash.Length);
            foreach (var character in hash)
            {
                sb.Append(character.ToString("x2"));
            }
            return sb.ToString();
        }

        private string Signature()
        {
            var message = _nonce + _app_id + _api_key;
            var signature = ConfigureHmac(_api_secret, message);
            return signature;
        }

        private void ConfigureHeaders(string signature)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Token {_api_key}");
            _client.DefaultRequestHeaders.Add("Access-Key", _api_key);
            _client.DefaultRequestHeaders.Add("Access-Nonce", _nonce.ToString());
            _client.DefaultRequestHeaders.Add("Access-Signature", signature);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
