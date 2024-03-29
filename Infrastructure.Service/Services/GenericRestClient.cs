﻿using System;
using System.Net;
using System.Threading.Tasks;
using Infrastructure.Service.Interface;
using RestSharp;

namespace Infrastructure.Service.Services
{
    public class GenericRestClient : IGenericRestClient
    {
        private readonly IRestClient _restClient;

        public GenericRestClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        /// <summary>
        /// For getting the resources from a web api
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <returns>A Task with result object of type T</returns>
        public async Task<T> Get<T>(string apiUrl) where T : class
        {
            var request = new RestRequest($"{apiUrl}", Method.GET);

            var response = await _restClient.ExecuteAsync<T>(request).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }

            throw new Exception("Api request failed");
        }

        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="postObject">The object to be created</param>
        /// <returns>A Task with created item</returns>
        public async Task<T> PostRequest<T>(string apiUrl, T postObject) where T : class
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

            throw new Exception("Api request failed");
        }

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="putObject">The object to be edited</param>
        public async Task<T> PutRequest<T>(string apiUrl, T putObject)
        {
            var request = new RestRequest($"{apiUrl}", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(putObject);

            var response = await _restClient.ExecuteAsync<T>(request).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return response.Data;
            }

            throw new Exception("Api request failed");
        }
    }
}