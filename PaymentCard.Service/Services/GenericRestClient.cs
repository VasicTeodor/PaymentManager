using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bank.Service.Services
{
    public class GenericRestClient : IGenericRestClient
    {
        private readonly IRestClient _restClient;

        public GenericRestClient(IRestClient restClient)
        {
            _restClient = restClient;
        }
        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="postObject">The object to be created</param>
        /// <returns>A Task with created item</returns>
        public async Task<T> PostRequest<T>(string apiUrl, object postObject) where T : class
        {
            var request = new RestRequest($"{apiUrl}", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(postObject);

            var response = await _restClient.ExecuteAsync<T>(request).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception("Api request failed");
            }
        }
    }
}
