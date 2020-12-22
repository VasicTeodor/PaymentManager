using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Services
{
    public interface IGenericRestClient
    {
        Task<T> PostRequest<T>(string apiUrl, object postObject) where T : class;
    }
}
