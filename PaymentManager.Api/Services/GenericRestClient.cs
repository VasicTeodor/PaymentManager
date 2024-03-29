﻿using System;
using System.Net;
using System.Threading.Tasks;
using PaymentManager.Api.Services.Interfaces;
using RestSharp;

namespace PaymentManager.Api.Services
{
    /// <summary>
    /// A generic wrapper class to REST API calls
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="putObject">The object to be edited</param>
        public async Task<T> PutRequest<T>(string apiUrl, object putObject) where T : class
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

        /// <summary>
        /// For deleting an existing item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public async Task<T> DeleteRequest<T>(string apiUrl) where T : class
        {
            var request = new RestRequest($"{apiUrl}", Method.DELETE);

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

        /// <summary>
        /// For crating a patch request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="patchObject"></param>
        /// <returns></returns>
        public async Task<T> PatchRequest<T>(string apiUrl, object patchObject) where T : class
        {
            var request = new RestRequest($"{apiUrl}", Method.PATCH)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(patchObject);

            var response = await _restClient.ExecuteAsync<T>(request).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Accepted)
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