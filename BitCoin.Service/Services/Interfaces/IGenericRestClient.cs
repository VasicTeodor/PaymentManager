using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Services.Interfaces
{
    public interface IGenericRestClient
    {
        Task<T> Get<T>(string apiUrl) where T : class;
        Task<T> PostRequest<T>(string apiUrl, object postObject) where T : class;
        Task<T> PutRequest<T>(string apiUrl, object putObject) where T : class;
        Task<T> PatchRequest<T>(string apiUrl, object patchObject) where T : class;
        Task<T> DeleteRequest<T>(string apiUrl) where T : class;
    }
}
